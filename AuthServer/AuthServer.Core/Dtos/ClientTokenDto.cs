using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Dtos
{
    //Client dediğimiz kullanıcı değil Apilerle iletişime geçecek olan aplikasyonlardır
    public class ClientTokenDto
    {
        //Endpointlere istek yapacak olan token tipi
        public string AccessToken { get; set; }
        //Access Tokenin Expire Zamanı
        public DateTime AccessTokenExpireDate { get; set; }
    }
}
