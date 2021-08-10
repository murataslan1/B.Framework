using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using B.Framework.Application.Attribute.Extensions;
using B.Framework.Domain.Shared.Attribute;
using B.Framework.Domain.Shared.Enums.Claims;
using B.Framework.Domain.Shared.Security;
using B.Framework.Domain.Shared.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace B.Framework.Application.Security.JWT
{
    public class BTokenHandler
    {
      
        private readonly JWTConfigurationBase _confBase;

        public BTokenHandler(IConfiguration configuration,IOptions<JWTConfigurationBase> confBase)
        {
            _confBase = confBase.Value;
       ;
        }
        

        public Token CreateAccessToken(UserBase user)
        {
            Token tokenInstance = new Token();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confBase.Key));
            SigningCredentials signingCredentials =
                new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            tokenInstance.Expiration = DateTime.Now.AddMinutes(JWTBaseDefinations.ExpirationMinute);

            var claimsList = new List<Claim>();
            claimsList.Add(new Claim(BEClaimTypes.FirstName.GetBEnumValue<BAttributeBase>(),user.Username));
            claimsList.Add(new Claim(BEClaimTypes.TenantId.GetBEnumValue<BAttributeBase>(),"545454"));
            foreach (var userRole in user.Roles)
            {
                claimsList.Add(new Claim(BEClaimTypes.Role.GetBEnumValue<BAttributeBase>(),userRole));
            }
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claimsList),
                Expires = tokenInstance.Expiration,
                SigningCredentials = signingCredentials,
                Audience = _confBase.Audience,
                Issuer = _confBase.Issuer
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            tokenInstance.AccessToken = tokenHandler.WriteToken(token);
            tokenInstance.RefreshToken = CreateRefreshToken();
            return tokenInstance;
        }


        public string CreateRefreshToken()
        {
            byte[] temp = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(temp);
            return Convert.ToBase64String(temp);
        }
    }
}