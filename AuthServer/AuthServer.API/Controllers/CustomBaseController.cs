using AuthServer.Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.API.Controllers
{
    //Diğer controller ın ortak kullanacağı özellikler
    public class CustomBaseController : ControllerBase
    {
        public IActionResult ActionResultInstance<T>(Response<T> response) where T : class
        {
            //ObjectResult  Ok(),BadRequest() gibi dönüş tiplerinin miras aldığı dönüş tipidir
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };

        }
    }
}
