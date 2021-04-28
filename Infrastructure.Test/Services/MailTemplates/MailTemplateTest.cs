using System;
using System.Reflection;
using Kaizen.Core.Services;
using Kaizen.Infrastructure.Services.MailTemplates;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Infrastructure.Test.Services.MailTemplates
{
    [TestFixture]
    public class MailTemplateTest
    {
        private IMailTemplate _mailTemplate;

        private ServiceProvider ServiceProvider { get; set; }

        [OneTimeSetUp]
        public void Init()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddLogging();

            ServiceProvider = services.BuildServiceProvider();
        }

        [SetUp]
        public void SetUp()
        {
            Mock<IHostEnvironment> hostEnvironment = new Mock<IHostEnvironment>();

            string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            hostEnvironment.Setup(h => h.ContentRootPath).Returns(path);

            _mailTemplate = new MailTemplate(hostEnvironment.Object,
                ServiceProvider.GetService<ILogger<MailTemplate>>());
        }

        [Test]
        public void Load_An_Existing_Template_Without_Params()
        {
            string template = _mailTemplate.LoadTemplate("Test.html");

            Assert.IsNotNull(template);
            Assert.IsTrue(template.Length > 0);
        }

        [Test]
        public void Load_An_Existing_Template_With_Params()
        {
            string template = _mailTemplate.LoadTemplate("TestWithParams.html", "World");

            Assert.IsNotNull(template);
            Assert.IsTrue(template.Length > 0);
            Assert.IsTrue(template.Contains("Hello World."));
        }

        [Test]
        public void Load_Non_Existent_Template()
        {
            try
            {
                _ = _mailTemplate.LoadTemplate("NonExistentTemplate.html");
                Assert.Fail("The template to search should not exist.");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Email template NonExistentTemplate.html does not exists.", e.Message);
            }
        }
    }
}
