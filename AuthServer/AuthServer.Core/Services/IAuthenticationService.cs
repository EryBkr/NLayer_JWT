using AuthServer.Core.Dtos;
using AuthServer.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Services
{
    public interface IAuthenticationService
    {
        //User lara  token ları  dönecek metot
        Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto);

        //Gönderdiğimiz Refresh Token aracılığıyla bizlere access token dönecek metot
        Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken);

        //Refresh token i sil
        Task<Response<NoData>> RevokeRefreshToken(string refreshToken);

        //Aplikasyonların apilere iletişimi için
        Task<Response<ClientTokenDto>> CreateTokenByClient(ClientLoginDto clientLogin);
    }
}
