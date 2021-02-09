using System;
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
    [Order(4)]
    public class ServicesRepositoryTest : BaseRepositoryTest
    {
        private IServicesRepository _servicesRepository;
        private ApplicationDbContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            _servicesRepository = ServiceProvider.GetService<IServicesRepository>();
            _dbContext = ServiceProvider.GetService<ApplicationDbContext>();
        }

        [Test]
        public void CheckServicesRepository()
        {
            Assert.IsNotNull(_servicesRepository);
        }

        [Test]
        public async Task CheckServiceTypes()
        {
            IEnumerable<ServiceType> serviceTypes = await _servicesRepository.GetServiceTypesAsync();
            Assert.IsTrue(serviceTypes.Any());
        }

        [Test]
        public async Task Save_Invalid_Service()
        {
            try
            {
                Service service = new Service();

                _servicesRepository.Insert(service);

                await _dbContext.SaveChangesAsync();

                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task Save_Valid_Service()
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

                _servicesRepository.Insert(service);

                await _dbContext.SaveChangesAsync();

                Assert.Pass();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.InnerException?.Message);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public async Task Search_Non_Existent_Service()
        {
            Service service = await _servicesRepository.FindByIdAsync("3333");

            Assert.IsNull(service);
        }

        [Test]
        public async Task Search_Existing_Service()
        {
            Service service = await _servicesRepository.FindByIdAsync("345RT");

            Assert.IsNotNull(service);
            Assert.AreEqual("345RT", service.Code);
            Assert.AreEqual("Control de plagas", service.Name);
        }

        [Test]
        public async Task Update_Existing_Service()
        {
            try
            {
                Service service = await _servicesRepository.FindByIdAsync("345RT");
                Assert.IsNotNull(service);

                service.Name = "Control de roedores";
                _servicesRepository.Update(service);

                await _dbContext.SaveChangesAsync();

                service = await _servicesRepository.FindByIdAsync("345RT");
                Assert.AreEqual("Control de roedores", service.Name);
            }
            catch (DbUpdateException e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
