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
        private IServiceRequestsRepository _serviceRequestsRepository;
        private ApplicationDbContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            _serviceRequestsRepository = ServiceProvider.GetService<IServiceRequestsRepository>();
            _dbContext = ServiceProvider.GetService<ApplicationDbContext>();
        }

        [Test]
        public void CheckServiceRequestsRepository()
        {
            Assert.IsNotNull(_serviceRequestsRepository);
        }

        [Test]
        public async Task Save_Valid_ServiceRequest()
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

                _serviceRequestsRepository.Insert(serviceRequest);

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
        public async Task Search_Non_Existent_ServiceRequest()
        {
            ServiceRequest serviceRequest = await _serviceRequestsRepository.FindByIdAsync(2);

            Assert.IsNull(serviceRequest);
        }

        [Test]
        public async Task Search_Existing_ServiceRequest()
        {
            ServiceRequest serviceRequest = await _serviceRequestsRepository.FindByIdAsync(1);

            Assert.IsNotNull(serviceRequest);
            Assert.AreEqual(PeriodicityType.Casual, serviceRequest.Periodicity);
            Assert.AreEqual(ServiceRequestState.Pending, serviceRequest.State);
        }

        [Test]
        public async Task Update_Existing_ServiceRequest()
        {
            ServiceRequest serviceRequest = await _serviceRequestsRepository.FindByIdAsync(1);
            Assert.IsNotNull(serviceRequest);

            serviceRequest.State = ServiceRequestState.Accepted;
            _serviceRequestsRepository.Update(serviceRequest);

            await _dbContext.SaveChangesAsync();

            serviceRequest = await _serviceRequestsRepository.FindByIdAsync(1);

            Assert.AreEqual(ServiceRequestState.Accepted, serviceRequest.State);
        }
    }
}
