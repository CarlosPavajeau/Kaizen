using System.Globalization;
using Kaizen.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

namespace Kaizen.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureCors(this IApplicationBuilder app)
        {
            app.UseCors(c =>
            {
                c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

        public static void ConfigureSupportedCultures(this IApplicationBuilder app)
        {
            CultureInfo[] supportedCultures = new[] { new CultureInfo("es-CO"), new CultureInfo("en-US") };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("es-CO"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
        }
    }
}
