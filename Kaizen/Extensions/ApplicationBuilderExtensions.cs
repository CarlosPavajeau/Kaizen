using Kaizen.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Kaizen.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
