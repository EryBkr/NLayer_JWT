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
        public string Id { get; set; }
        public string Secret { get; set; }

        //Hangi Apilere erişim olacak
        public List<string> Audiences { get; set; }
    }
}
