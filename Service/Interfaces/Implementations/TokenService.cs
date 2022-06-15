using Microsoft.IdentityModel.Tokens;
using Service.Configurations;
using Service.Interfaces.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TODOLIST.Domain.Models;

namespace Service.Interfaces.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly TokenConfigurations _configuration;

        public TokenService(TokenConfigurations tokenConfiguration)
        {
            _configuration = tokenConfiguration;
        }
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, user.FullName.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(_configuration.TimeToExpiry),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
