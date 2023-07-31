using IntraCommunicationWebApi.Model;
using IntraCommunicationWebApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace IntraCommunicationWebApi.Repositories
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly InterCommunicationDBContext db;
        public JWTManagerRepository(InterCommunicationDBContext db, IConfiguration configuration) 
        {
            this.db = db;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public Tokens Authenticate(SignInViewModel user)
        {
            var isUser = db.UserProfiles.Where(u => u.Email == user.Email).FirstOrDefault();

            if (isUser != null && BCrypt.Net.BCrypt.Verify(user.Password, isUser.Password))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Email, user.Email),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new Tokens { Token = tokenHandler.WriteToken(token) };
            }

            return null;
            
        }
    }
}
