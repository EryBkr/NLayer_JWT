using AuthServer.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Service.Extensions
{
    public static class CustomValidationResponse
    {
        //Dönen hata modelini değiştiriyorum ve bana uygun hale getiriyorum
        public static void UseCustomValidationResponse(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.Configure<ApiBehaviorOptions>(opt => 
            {
                opt.InvalidModelStateResponseFactory = context => 
                {
                    var errors = context.ModelState.Values.Where(i => i.Errors.Count() > 0).SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

                    ErrorDto errorDto = new ErrorDto(errors.ToList(),true);
                    var response = Response<NoData>.Fail(errorDto, 400);
                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
