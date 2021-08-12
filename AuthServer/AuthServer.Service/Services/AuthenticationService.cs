using AuthServer.Core.Configuration;
using AuthServer.Core.Dtos;
using AuthServer.Core.Entities;
using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _uOw;
        private readonly IGenericRepository<UserRefreshToken> _genericService;

        public AuthenticationService(IOptions<List<Client>> clients, ITokenService tokenService, UserManager<User> userManager, IUnitOfWork uOw, IGenericRepository<UserRefreshToken> genericService)
        {
            _clients = clients.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _uOw = uOw;
            _genericService = genericService;
        }

        public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentException(nameof(loginDto));

            //Email e ait kayıt var mı
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Response<TokenDto>.Fail("Email veya Password Yanlış", 400, true);

            //Şifre yanlış ise
            if (!(await _userManager.CheckPasswordAsync(user, loginDto.Password)))
                return Response<TokenDto>.Fail("Email veya Password Yanlış", 400, true);

            //Tokeni oluşturduk
            var token = _tokenService.CreateToken(user);

            //Refresh token var mı
            var userRefreshToken = await _genericService.Where(i => i.UserId == user.Id).SingleOrDefaultAsync();

            //Yoksa oluşturuyorum
            if (userRefreshToken == null)
            {
                await _genericService.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, ExpireDate = token.RefreshTokenExpireDate });
            }
            else
            {
                //Varsa güncelliyorum
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.ExpireDate = token.RefreshTokenExpireDate;
            }

            await _uOw.CommitAsync();

            return Response<TokenDto>.Success(token, 200);
        }

        public Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLogin)
        {
            if (clientLogin == null) throw new ArgumentException(nameof(clientLogin));

            //appsettings e kaydettiğimiz Client ların(aplikasyonlar)  içerisinderinden bize istek yapan client ı alıyoruz ve ona özel token oluşturuyoruz
            var client = _clients.SingleOrDefault(x => x.Id == clientLogin.ClientId && x.Secret == clientLogin.ClientSecret);

            if (client == null) return Response<ClientTokenDto>.Fail("ClientId or ClientSecret Not Found", 400, true);

            var token = _tokenService.CreateTokenByClient(client);

            return Response<ClientTokenDto>.Success(token,200);

        }

        public async Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            //Database de bana ait refresh token var mı?
            var myRefreshToken = await _genericService.Where(i => i.Code == refreshToken).SingleOrDefaultAsync();

            if (myRefreshToken == null) return Response<TokenDto>.Fail("Refresh token  not found", 404, true);

            //Refresh token tablomuzda ki o tokene ait kullanıcıyı aldık
            var user = await _userManager.FindByIdAsync(myRefreshToken.UserId);

            if(user==null) return Response<TokenDto>.Fail("User  not found", 404, true);

            //Kullanıcıya ait token i oluşturuyorum.Refresh token gönderdiği için giriş yapmasına gerek kalmadı 
            var token = _tokenService.CreateToken(user);
            myRefreshToken.Code = token.RefreshToken;
            myRefreshToken.ExpireDate = token.RefreshTokenExpireDate;

            await _uOw.CommitAsync();

            return Response<TokenDto>.Success(token, 200);
        }

        //Refresh token silinmek istenirse
        public async Task<Response<NoData>> RevokeRefreshToken(string refreshToken)
        {
            //Db de refresh token var mı
            var myRefreshToken = await _genericService.Where(i => i.Code == refreshToken).SingleOrDefaultAsync();

            if (myRefreshToken == null) Response<NoData>.Fail("Refresh Token Yok",404,true);

            //Refreh tokeni db den siliyorum
            _genericService.Remove(myRefreshToken);

            await _uOw.CommitAsync();

            return Response<NoData>.Success(200);
        }
    }
}
