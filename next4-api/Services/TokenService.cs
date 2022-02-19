using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using next4_api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace next4_api.Services
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
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds    
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}