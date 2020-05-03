using Kaizen.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Kaizen.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureGlobalFilters(this IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                config.Filters.Add(new ModelStateFilterAttribute());
            });
        }
    }
}
