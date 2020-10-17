using System;
using Kaizen.Domain.Data;
using Kaizen.Domain.Data.Configuration;
using Kaizen.Domain.Extensions;
using Kaizen.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Infrastructure.Test.Repositories
{
    [TestFixture]
    public class BaseRepositoryTest
    {
        protected ServiceProvider ServiceProvider { get; private set; }

        [OneTimeSetUp]
        public void Init()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true)
                                                          .AddEnvironmentVariables()
                                                          .Build();

            ServiceCollection services = new ServiceCollection();
            services.AddOptions();
            services.AddSingleton(configuration);
            services.Configure<Data>(c =>
            {
                c.Provider = (DataProvider)Enum.Parse(typeof(DataProvider), configuration["Data:Provider"]);
            });
            services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
            services.AddEntityFramework(configuration);
            services.ConfigureRepositories();

            ServiceProvider = services.BuildServiceProvider();
            ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
        }
    }
}
