using JwtAuth.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JwtAuth.Utils
{
    public static class Token
    {
        public static User DeserializeToken(this HttpContext context)
        {
            return JsonConvert.DeserializeObject<User>(context.User.Claims.FirstOrDefault(i => i.Type == "user").Value);
        }

        public static string SetToken(this JwtSecurityTokenHandler tokenHendler, string key, User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("user",  JsonConvert.SerializeObject(user))
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHendler.CreateToken(tokenDescriptor);
            return tokenHendler.WriteToken(token);
        }

        public static TokenValidationParameters GetTokenValidation(string key)
        {
            return new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                ValidateIssuerSigningKey = true
            };
        }
    }
}
