using Kaizen.Core.Security;
using Kaizen.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Infrastructure.Test.Security
{
    [TestFixture]
    public class TokenGeneratorTest
    {
        ITokenGenerator tokenGenerator;

        [OneTimeSetUp]
        public void Init()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true)
                                                          .AddEnvironmentVariables()
                                                          .Build();

            ServiceCollection services = new ServiceCollection();
            services.AddOptions();
            services.AddSingleton(configuration);
            services.ConfigureTokenGenerator();

            var serviceProvider = services.BuildServiceProvider();
            tokenGenerator = serviceProvider.GetService<ITokenGenerator>();
        }

        [Test]
        public void CheckTokenGenerator()
        {
            Assert.IsNotNull(tokenGenerator);
        }

        [Test]
        public void GenerateToken()
        {
            string token = tokenGenerator.GenerateToken("admin", "Administrator");
            Assert.IsTrue(token.Length > 0);
        }
    }
}
