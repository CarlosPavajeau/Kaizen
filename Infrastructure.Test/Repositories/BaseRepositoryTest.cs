using System;
using System.Linq;
using Kaizen.Domain.Data;
using Kaizen.Domain.Data.Configuration;
using Kaizen.Domain.Extensions;
using Kaizen.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
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
            services.AddIdentityConfig();
            services.AddLogging();
            services.ConfigureApplicationServices();
            services.ConfigureMailTemplates();
            services.LoadMailSettings(configuration);

            Mock<IHostEnvironment> mockHostingEnvironment = new Mock<IHostEnvironment>();
            services.AddSingleton(mockHostingEnvironment.Object);

            ServiceProvider = services.BuildServiceProvider();
            ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
        }

        protected void DetachAllEntities()
        {
            ApplicationDbContext dbContext = ServiceProvider.GetService<ApplicationDbContext>();
            dbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .ToList()
                .ForEach(e =>
                {
                    try
                    {
                        e.State = EntityState.Detached;
                    }
                    catch (Exception)
                    {
                        // Ignore exception
                    }
                });
        }
    }
}
