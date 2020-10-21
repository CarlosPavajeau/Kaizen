using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Infrastructure.Test.Repositories
{
    [TestFixture]
    public class ServicesRepositoryTest : BaseRepositoryTest
    {
        private IServicesRepository servicesRepository;
        private ApplicationDbContext dbContext;

        [SetUp]
        public void SetUp()
        {
            servicesRepository = ServiceProvider.GetService<IServicesRepository>();
            dbContext = ServiceProvider.GetService<ApplicationDbContext>();
        }

        [Test]
        public void CheckServicesRepository()
        {
            Assert.IsNotNull(servicesRepository);
        }

        [Test]
        public async Task CheckServiceTypes()
        {
            IEnumerable<ServiceType> serviceTypes = await servicesRepository.GetServiceTypesAsync();
            Assert.IsTrue(serviceTypes.Count() > 0);
        }

        [Test]
        public async Task SaveService()
        {
            try
            {
                Service service = new Service
                {
                    Code = "345RT",
                    Name = "Control de plagas",
                    ServiceTypeId = 1,
                    Cost = 45000
                };

                servicesRepository.Insert(service);

                await dbContext.SaveChangesAsync();

                Assert.Pass();
            }
            catch (DbUpdateException)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task SearchService()
        {
            Service service = await servicesRepository.FindByIdAsync("345RT");

            Assert.IsNotNull(service);
            Assert.AreEqual("345RT", service.Code);
            Assert.AreEqual("Control de plagas", service.Name);
        }

        [Test]
        public async Task UpdateService()
        {
            try
            {
                Service service = await servicesRepository.FindByIdAsync("345RT");
                Assert.IsNotNull(service);

                service.Name = "Control de roedores";
                servicesRepository.Update(service);

                await dbContext.SaveChangesAsync();

                service = await servicesRepository.FindByIdAsync("345RT");
                Assert.AreEqual("Control de roedores", service.Name);
            }
            catch (DbUpdateException)
            {
                Assert.Fail();
            }
        }
    }
}
