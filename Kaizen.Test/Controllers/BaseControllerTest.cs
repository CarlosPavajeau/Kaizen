using System;
using AutoMapper;
using Kaizen.Domain.Data.Configuration;
using Kaizen.Domain.Extensions;
using Kaizen.Extensions;
using Kaizen.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using NUnit.Framework;

namespace Kaizen.Test.Controllers
{
    [TestFixture]
    public class BaseControllerTest
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
            services.ConfigureTokenGenerator();

            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup));
            services.ConfigureDomainEventDispatcher();
            services.ConfigureGlobalFilters();
            services.ConfigureApplicationServices();

            Mock<IHostEnvironment> mockHostingEnvironment = new Mock<IHostEnvironment>();
            services.AddSingleton(mockHostingEnvironment.Object);

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
