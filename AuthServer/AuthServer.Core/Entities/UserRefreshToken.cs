using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Entities
{
    //Refresh Token Table Entity
    public class UserRefreshToken
    {
        public string UserId { get; set; }
        public string Code { get; set; } //RefreshToken
        public DateTime ExpireDate { get; set; }
    }
}
