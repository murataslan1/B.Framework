namespace B.Framework.Domain.Shared.Security
{
    public class JWTBaseDefinations
    {

        public static string ConfigurationName = "JWTSettings";

        public static int ExpirationMinute = 60 * 5;

        public class OpenApiSecurityScheme
        {
            public const string Scheme = "Bearer";
            public const string BearerFormat = "JWT";
            public const string Name = "JWT Authentication";
            public const string Description = "**Token** değerini giriniz ";
        }
    }
}