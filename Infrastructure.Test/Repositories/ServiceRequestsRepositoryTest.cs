using System;
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
    [Order(5)]
    public class ServiceRequestsRepositoryTest : BaseRepositoryTest
    {
        private IServiceRequestsRepository serviceRequestsRepository;
        private ApplicationDbContext dbContext;

        [SetUp]
        public void SetUp()
        {
            serviceRequestsRepository = ServiceProvider.GetService<IServiceRequestsRepository>();
            dbContext = ServiceProvider.GetService<ApplicationDbContext>();
        }

        [Test]
        public void CheckServiceRequestsRepository()
        {
            Assert.IsNotNull(serviceRequestsRepository);
        }

        [Test]
        public async Task SaveServiceRequest()
        {
            try
            {
                ServiceRequest serviceRequest = new ServiceRequest
                {
                    Date = DateTime.Now,
                    ClientId = "12345678",
                    Periodicity = PeriodicityType.Casual,
                    State = ServiceRequestState.Pending
                };

                serviceRequestsRepository.Insert(serviceRequest);

                await dbContext.SaveChangesAsync();

                Assert.Pass();
            }
            catch (DbUpdateException)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task SearchServiceRequest()
        {
            ServiceRequest serviceRequest = await serviceRequestsRepository.FindByIdAsync(1);

            Assert.IsNotNull(serviceRequest);
            Assert.AreEqual(PeriodicityType.Casual, serviceRequest.Periodicity);
            Assert.AreEqual(ServiceRequestState.Pending, serviceRequest.State);
        }

        [Test]
        public async Task UpdateServiceRequest()
        {
            ServiceRequest serviceRequest = await serviceRequestsRepository.FindByIdAsync(1);
            Assert.IsNotNull(serviceRequest);

            serviceRequest.State = ServiceRequestState.Accepted;
            serviceRequestsRepository.Update(serviceRequest);

            await dbContext.SaveChangesAsync();

            serviceRequest = await serviceRequestsRepository.FindByIdAsync(1);

            Assert.AreEqual(ServiceRequestState.Accepted, serviceRequest.State);
        }
    }
}
