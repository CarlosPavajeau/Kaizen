using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Kaizen.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerApiDocumentation(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint(configuration["Swagger:Url"], configuration["Swagger:Name"]);
            });

            return app;
        }
    }
}
