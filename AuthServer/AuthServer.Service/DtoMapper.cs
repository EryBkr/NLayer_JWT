using AuthServer.Core.Dtos;
using AuthServer.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Service
{
    class DtoMapper:Profile
    {
        public DtoMapper()
        {
            //Her iki taraflı dönüşümü gerçekleştirdim
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
