using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Entities
{
    //Identity den kalıtım alan Kullanıcı sınıfımı oluşturdum
    public class User:IdentityUser
    {
        public string City { get; set; }
    }
}
