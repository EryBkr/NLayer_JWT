using AuthServer.Core.Dtos;
using AuthServer.Core.Entities;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        //Kullanıcı Kaydı
        public async Task<Response<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new User { Email = createUserDto.Email, UserName = createUserDto.UserName };

            //Kullanıcıyı oluşturuyorum
            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                //Identity den aldığımız hataları dönüyoruz
                var errors = result.Errors.Select(i => i.Description).ToList();
                return Response<UserDto>.Fail(new ErrorDto(errors, true), 400);
            }

            //Hata yoksa UserDto yu dönüyoruz
            return Response<UserDto>.Success(ObjectMapper.Mapper.Map<UserDto>(user), 200);
        }

        public async Task<Response<UserDto>> GetUserByNameAsync(string userName)
        {
            //Kullanıcı adından user a ulaşıyoruz
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null) return Response<UserDto>.Fail("Username not found", 404, true);

            //Hata yoksa UserDto yu dönüyoruz
            return Response<UserDto>.Success(ObjectMapper.Mapper.Map<UserDto>(user), 200);
        }
    }
}
