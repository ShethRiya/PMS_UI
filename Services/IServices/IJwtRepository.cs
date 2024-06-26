using ProjectManagement_UI.Models.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Domain.Auth.Interface
{
    public interface IJwtRepository 
    {
        public string GenerateJwtToken(SessionUser user);
        public bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken);
    }
}
