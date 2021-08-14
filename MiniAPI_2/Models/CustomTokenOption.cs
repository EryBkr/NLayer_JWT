namespace MiniAPI_2.Models
{
    //Bu API e ait token ayarlarını bind edeceğim Token class ım
    public class CustomTokenOption
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}