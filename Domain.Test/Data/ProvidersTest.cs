using Kaizen.Domain.Data;
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
        private ApplicationDbContext _dbContext;

        [OneTimeSetUp]
        public void Init()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            ServiceCollection services = new ServiceCollection();
            services.AddOptions();
            services.LoadDbSettings(configuration);
            services.RegisterDbContext(configuration);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            _dbContext = serviceProvider.GetService<ApplicationDbContext>();
        }

        [Test]
        public void CheckDbContext()
        {
            Assert.NotNull(_dbContext);
            Assert.AreEqual("Microsoft.EntityFrameworkCore.Sqlite", _dbContext.Database.ProviderName,
                "The ApplicationDbContext mush be have a Sqlite provider");
            Assert.IsTrue(_dbContext.Database.IsSqlite());
        }
    }
}
