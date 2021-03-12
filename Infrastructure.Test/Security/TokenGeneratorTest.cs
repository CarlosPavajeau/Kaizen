using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
        private ITokenGenerator _tokenGenerator;

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

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            _tokenGenerator = serviceProvider.GetService<ITokenGenerator>();
        }

        [Test]
        public void CheckTokenGenerator()
        {
            Assert.IsNotNull(_tokenGenerator);
        }

        [Test]
        public void Generate_And_Verify_Token()
        {
            var token = _tokenGenerator.GenerateToken("123", "admin", "Administrator");
            Assert.IsTrue(token.Length > 0);

            var handler = new JwtSecurityTokenHandler();
            var tokenRead = handler.ReadToken(token) as JwtSecurityToken;
            Assert.IsNotNull(tokenRead);

            var tokenRole = tokenRead.Claims.First(claim => claim.Type == "role").Value;
            Assert.AreEqual("Administrator", tokenRole);
        }
    }
}
