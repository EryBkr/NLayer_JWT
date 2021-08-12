using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Service
{
    public static class ObjectMapper
    {
        //Ben çağırmadığım sürece memory de bulunmaması için lazy kullandım
        //Çünkü static bir metot
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
             {
                 var config = new MapperConfiguration(cfg =>
                 {
                     cfg.AddProfile<DtoMapper>();
                 });
                 return config.CreateMapper();
             });

        //MapProfile için çağırabilirim
        public static IMapper Mapper => lazy.Value;
    }
}
