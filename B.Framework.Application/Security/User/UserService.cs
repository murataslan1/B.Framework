using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using B.Framework.Application.Security.JWT;
using B.Framework.Domain.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace B.Framework.Application.Security.User
{
    public class UserService : IUserService
    {
        private readonly  JWTConfigurationBase _jwtConf;

        public UserService(IOptions<JWTConfigurationBase> jwtConf)
        {
            _jwtConf = jwtConf.Value;
        }
        public Tuple<string,bool> Authenticate(string username, string password)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConf.Key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "burak"),
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim(ClaimTypes.Role,"Admin2")
                    }
                ),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tuple<string, bool>(tokenHandler.WriteToken(token), true);
        }
    }
}