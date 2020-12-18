using AppDocker.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reactive.Subjects;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppDocker.Services
{
    public static class TokenService
    {
        public static string GenerateToken(Usuarios usuarios)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, usuarios.Email.ToString()),
                new Claim(ClaimTypes.SerialNumber, usuarios.Password.ToString()),
            }),
               Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = 
                new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;

        }
    }
}
