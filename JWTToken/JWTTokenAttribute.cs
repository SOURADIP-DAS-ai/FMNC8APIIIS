using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Security.Principal;

namespace BHSAPIBaseNC8.JWTToken
{
    public class JWTTokenAttribute : Attribute, IAuthorizationFilter
    {
        public string Realm { get; set; }
        public bool AllowMultiple => false;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authorization) || !authorization.StartsWith("Bearer "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = authorization.Substring("Bearer ".Length).Trim();

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var principal = AuthenticateJwtToken(token);

            if (principal == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            context.HttpContext.User = principal;
        }//eof

        private static bool ValidateToken(string token, out string username)
        {
            username = null;

            var simplePrinciple = JwtManager.GetPrincipal(token);
            var identity = simplePrinciple?.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim?.Value;

            if (string.IsNullOrEmpty(username))
                return false;

            // More validate to check whether username exists in system

            return true;
        }//eof

        protected ClaimsPrincipal AuthenticateJwtToken(string token)
        {
            ClaimsPrincipal user = null;
            if (ValidateToken(token, out var username))
            {
                // based on username to get more information from database in order to build local identity
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                    // Add more claims if needed: Roles, ...
                };

                var identity = new ClaimsIdentity(claims, "Jwt");
                user = new ClaimsPrincipal(identity);                
            }

            return (user);
        }//eof

    }
}
