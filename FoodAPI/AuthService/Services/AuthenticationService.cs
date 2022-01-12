using AuthService.Exceptions;
using AuthService.Models;
using AuthService.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly string tokenKey;
        private readonly IUserRepository _userRepository;


        public AuthenticationService(IUserRepository userRepository, IConfiguration configuration)
        {
            this.tokenKey = configuration.GetValue<string>("TokenKey");
            this._userRepository = userRepository;
        }

        public string Authenticate(string username, string password)
        {

            User obj = _userRepository.GetUserByUserNameAndPassword(username, password);
            if (obj == null)
            {
                throw new InvalidCredentialsException("Invalid User Credentials");
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

