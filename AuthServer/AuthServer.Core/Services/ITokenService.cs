using AuthServer.Core.Configuration;
using AuthServer.Core.Dtos;
using AuthServer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Services
{
    //Token İşlemleri için Interface
    public interface ITokenService
    {
        //Kullanıcıya özgü access ve refresh token oluşturacak
        TokenDto CreateToken(User user);

        //Client dediğimiz kullanıcı değil Apilerle iletişime geçecek olan aplikasyonlardır
        ClientTokenDto CreateTokenByClient(Client client);
    }
}
