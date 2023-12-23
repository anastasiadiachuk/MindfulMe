using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MindfulMe.Middlewares
{
    
    public class AuthRequiredMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthRequiredMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Please log in to access this resource");
            }
        }
    }

    public static class AuthRequiredMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthRequired(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthRequiredMiddleware>();
        }
    }
}
