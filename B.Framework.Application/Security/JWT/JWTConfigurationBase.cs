namespace B.Framework.Application.Security.JWT
{
    public class JWTConfigurationBase
    {
        public string Key { get; set; }

        public string Audience { get; set; }

        public string Issuer { get; set; }
        
        
    }
}