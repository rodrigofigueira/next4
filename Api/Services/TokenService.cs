using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Api.Services
{
    public class TokenService
    {

        private readonly SymmetricSecurityKey _key;

        public TokenService()
        {
            this._key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret unguessable key"));
        }

        public string CreateToken(string userName){

            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId, userName)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = creds    
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}