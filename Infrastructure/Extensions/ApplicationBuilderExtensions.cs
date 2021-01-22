using Microsoft.AspNetCore.Builder;

namespace Kaizen.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        private const string SwaggerUrl = "/swagger/v1/swagger.json";
        private const string SwaggerApiName = "My API version-1";

        public static IApplicationBuilder UseSwaggerApiDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint(SwaggerUrl, SwaggerApiName);
            });

            return app;
        }
    }
}
