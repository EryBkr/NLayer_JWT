using AuthServer.API.Models;
using AuthServer.Core.Dtos;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        //Kullanıcı oluşturuyoruz
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            return ActionResultInstance(await _userService.CreateUserAsync(createUserDto));
        }


        [HttpGet]
        [Authorize]
        //Giriş yapmış Kullanıcı ismine göre kullanıcı bilgilerini alıyoruz
        //Giriş yapmamış kullanıcılar haliyle ulaşamayacaklar
        public async Task<IActionResult> GetUser()
        {
            //Gelen token içerisinden NameClaim değerini bu şekilde alabiliyoruz
            var userName = HttpContext.User.Identity.Name;

            return ActionResultInstance(await _userService.GetUserByNameAsync(userName));
        }
    }
}
