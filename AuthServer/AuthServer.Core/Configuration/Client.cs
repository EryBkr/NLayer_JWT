using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Configuration
{
    //Aplikasyonları temsil ediyor
    public class Client
    {
        //Aplikasyon kimlik bilgileri
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        //Hangi Apilere erişim olacak
        public List<string> Audience { get; set; }
    }
}
