using System;
using Kaizen.Domain.Data;
using Kaizen.Domain.Data.Configuration;
using Kaizen.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Domain.Test.Data
{
    [TestFixture]
    public class ProvidersTest
    {
        ApplicationDbContext dbContext;

        [OneTimeSetUp]
        public void Init()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true)
                                                          .AddEnvironmentVariables()
                                                          .Build();

            ServiceCollection services = new ServiceCollection();
            services.AddOptions();
            services.Configure<Kaizen.Domain.Data.Configuration.Data>(c =>
            {
                c.Provider = (DataProvider)Enum.Parse(typeof(DataProvider), configuration["Data:Provider"]);
            });
            services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
            services.AddEntityFramework(configuration);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            dbContext = serviceProvider.GetService<ApplicationDbContext>();
        }

        [Test]
        public void CheckDbContext()
        {
            Assert.NotNull(dbContext);
            Assert.AreEqual("Microsoft.EntityFrameworkCore.Sqlite", dbContext.Database.ProviderName, "The ApplicationDbContext mush be have a Sqlite provider");
            Assert.IsTrue(dbContext.Database.IsSqlite());
        }
    }
}
