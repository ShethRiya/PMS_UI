using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using Domain.Auth.Interface;

using Task = System.Threading.Tasks.Task;
using ProjectManagement_UI.Models.Auth;

namespace Infrastructure.Auth.Repositories
{
    public class JwtRepository : IJwtRepository
    {
        private readonly IConfiguration _config;
        public JwtRepository( IConfiguration configuration)
        {
            _config = configuration;

        }

        

        public string GenerateJwtToken(SessionUser user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("roleId", user.RoleId.ToString()),
                new Claim("userName", user.UserName),
                new Claim("userId", user.UserId.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(1);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims, expires: expires,
                signingCredentials: credential
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken)
        {
            jwtSecurityToken = null;

            if (token == null)
            {
                return false;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            try
            {

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidAudience = _config["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                jwtSecurityToken = (JwtSecurityToken)validatedToken;

                if (jwtSecurityToken != null)
                {
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }


        }
    }
}
