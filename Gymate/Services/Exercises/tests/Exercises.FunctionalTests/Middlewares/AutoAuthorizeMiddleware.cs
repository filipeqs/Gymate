using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Exercises.FunctionalTests.Middlewares
{
    public class AutoAuthorizeMiddleware
    {
        private readonly RequestDelegate _next;

        public AutoAuthorizeMiddleware(RequestDelegate rd)
        {
            _next = rd;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var identity = new ClaimsIdentity("Bearer");

            identity.AddClaim(new Claim("role", "Admin"));

            httpContext.User.AddIdentity(identity);

            await _next(httpContext);
        }
    }
}
