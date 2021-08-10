﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Dtos
{
    //Genel Token Sınıfımız
    public class TokenDto
    {
        //Endpointlere istek yapacak olan token tipi
        public string AccessToken { get; set; }
        //Access Tokenin Expire Zamanı
        public DateTime AccessTokenExpireDate { get; set; }
        //Refresh Token değerimiz
        public string RefreshToken { get; set; }
        //Refresh Token Expire zamanı
        public DateTime RefreshTokenExpireDate { get; set; }
    }
}
