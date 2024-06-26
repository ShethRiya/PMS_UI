using Domain.Auth.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace ProjectManagement_UI.Services.AuthServices
{
    public class CustomAuthorize : Attribute, IAuthorizationFilter
    {
        private readonly int _roleId;

        public CustomAuthorize(int roleId = 0)
        {
            _roleId = roleId;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (_roleId == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
                return;
            }

            var jwtService = context.HttpContext.RequestServices.GetService<IJwtRepository>();

            if (jwtService == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
                return;
            }

            var request = context.HttpContext.Request;
            var token = request.Cookies["pms"];

            if (token == null || !jwtService.ValidateToken(token, out JwtSecurityToken jwtToken))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
                return;
            }

            var roleClaim = jwtToken.Claims.FirstOrDefault(claims => claims.Type == "roleId");
            var userId = jwtToken.Claims.FirstOrDefault(claims => claims.Type == "userId");
            var userName = jwtToken.Claims.FirstOrDefault(claims => claims.Type == "userName");

            context.HttpContext.Request.Headers.Add("userId", userId.Value);
            context.HttpContext.Request.Headers.Add("userName", userName.Value);

            if (roleClaim == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
                return;
            }

            if (!(_roleId == Convert.ToInt32(roleClaim.Value)))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "AccessDenied" }));
                return;
            }

        }
    }
}
