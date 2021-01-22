using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kaizen.Domain.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kaizen.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string dataProviderConfig = configuration.GetSection("Data")["Provider"];
            string connectionStringConfig = configuration.GetConnectionString("DefaultConnection");
            Assembly currentAssembly = typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly;
            IEnumerable<IDataProvider> dataProviders = currentAssembly.GetImplementationsOf<IDataProvider>();

            IDataProvider dataProvider = dataProviders.SingleOrDefault(x => x.Provider.ToString() == dataProviderConfig);

            return dataProvider?.RegisterDbContext(services, connectionStringConfig);
        }

        private static IEnumerable<T> GetImplementationsOf<T>(this Assembly assembly)
        {
            List<Type> types = assembly.GetTypes()
                .Where(t => t.GetTypeInfo().IsClass && !t.GetTypeInfo().IsAbstract && typeof(T).IsAssignableFrom(t))
                .ToList();

            return types.Select(type => (T)Activator.CreateInstance(type)).ToList();
        }
    }
}
