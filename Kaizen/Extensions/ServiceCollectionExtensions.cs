using Kaizen.Core.Domain;
using Kaizen.DomainEvents;
using Kaizen.Filters;
using Kaizen.HostedServices;
using Kaizen.HostedServices.ProcessingServices;
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

        public static void ConfigureHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<EmployeeContractHostedService>();
            services.AddHostedService<OverdueBillsHostedService>();
            services.AddHostedService<PendingActivitiesToBeConfirmedHostedService>();

            services.AddScoped(typeof(EmployeeContract));
            services.AddScoped(typeof(OverdueBills));
            services.AddScoped(typeof(PendingActivitiesToConfirmed));
        }

        public static void ConfigureDomainEventDispatcher(this IServiceCollection services)
        {
            services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
        }
    }
}
