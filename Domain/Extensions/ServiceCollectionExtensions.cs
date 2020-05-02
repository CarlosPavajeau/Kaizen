using Kaizen.Domain.Data;
using Kaizen.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Kaizen.Domain.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
		{
			string dataProviderConfig = configuration.GetSection("Data")["Provider"];
			string connectionStringConfig = configuration.GetConnectionString("DefaultConnection");
			Assembly currentAssenbly = typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly;
			IEnumerable<IDataProvider> dataProviders = currentAssenbly.GetImplementationsOf<IDataProvider>();

			IDataProvider dataProvider = dataProviders.SingleOrDefault(x => x.Provider.ToString() == dataProviderConfig);

			dataProvider.RegisterDbContext(services, connectionStringConfig);

			return services;
		}

		private static IEnumerable<T> GetImplementationsOf<T>(this Assembly assembly)
		{
			List<T> result = new List<T>();

			List<Type> types = assembly.GetTypes()
				.Where(t => t.GetTypeInfo().IsClass && !t.GetTypeInfo().IsAbstract && typeof(T).IsAssignableFrom(t))
				.ToList();

			foreach (Type type in types)
			{
				T instance = (T)Activator.CreateInstance(type);
				result.Add(instance);
			}

			return result;
		}

		public static void ConfigureRepositories(this IServiceCollection services)
		{
			services.AddScoped<IUnitWork, UnitWork>();
			services.AddScoped<IClientsRepository, ClientsRepository>();
			services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
			services.AddScoped<IEmployeesRepository, EmployeesRepository>();
			services.AddScoped<IProductsRepository, ProductsRepository>();
			services.AddScoped<IEquipmentsRepository, EquipmentsRepository>();
			services.AddScoped<IServicesRepository, ServicesRepository>();
		}
	}
}
