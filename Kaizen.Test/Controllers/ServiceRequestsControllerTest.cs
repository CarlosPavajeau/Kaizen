using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.ServiceRequest;
using Kaizen.Test.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Kaizen.Test.Controllers
{
    [TestFixture]
    public class ServiceRequestsControllerTest : BaseControllerTest
    {
        private ServiceRequestsController _serviceRequestsController;
        private Mock<IServiceRequestsRepository> _serviceRequestsRepository;
        private Mock<IUnitWork> _unitWork;

        [SetUp]
        public void SetUp()
        {
            _serviceRequestsRepository = new Mock<IServiceRequestsRepository>();
            _unitWork = new Mock<IUnitWork>();

            _serviceRequestsController = new ServiceRequestsController(_serviceRequestsRepository.Object,
                _unitWork.Object, ServiceProvider.GetService<IMapper>());

            SetupServiceRequestsRepository();
            SetUpUnitWork();
        }

        private void SetupServiceRequestsRepository()
        {
            _serviceRequestsRepository.Setup(r => r.GetAll()).Returns(new TestAsyncEnumerable<ServiceRequest>(
                new List<ServiceRequest>
                {
                    new()
                    {
                        Code = 1,
                        ClientId = "1007870922",
                        Client = new Client
                        {
                            Id = "1007870922"
                        }
                    },
                    new()
                    {
                        Code = 2,
                        ClientId = "1007870921",
                        Client = new Client
                        {
                            Id = "1007870921"
                        }
                    }
                }));

            _serviceRequestsRepository.Setup(r => r.FindByIdAsync(1)).ReturnsAsync(new ServiceRequest
            {
                Code = 1,
                ClientId = "1007870922",
                Client = new Client
                {
                    Id = "1007870922"
                }
            });
            _serviceRequestsRepository.Setup(r => r.FindByIdAsync(3)).ReturnsAsync((ServiceRequest)null);

            _serviceRequestsRepository.Setup(r => r.GetPendingCustomerServiceRequest("1007870922")).ReturnsAsync(new ServiceRequest
            {
                Code = 1,
                ClientId = "1007870922",
                Client = new Client
                {
                    Id = "1007870922"
                }
            });
            _serviceRequestsRepository.Setup(r => r.GetPendingCustomerServiceRequest("1007870919"))
                .ReturnsAsync((ServiceRequest)null);

            _serviceRequestsRepository.Setup(r => r.Update(It.IsAny<ServiceRequest>())).Verifiable();

            _serviceRequestsRepository.Setup(r => r.Insert(It.IsAny<ServiceRequest>())).Verifiable();
        }

        private void SetUpUnitWork()
        {
            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);
        }

        [Test]
        public async Task Get_ServiceRequests()
        {
            OkObjectResult result = (await _serviceRequestsController.GetServiceRequests()).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<ServiceRequestViewModel>>(result.Value);
        }

        [Test]
        public async Task Get_Existing_ServiceRequest()
        {
            ActionResult<ServiceRequestViewModel> result = await _serviceRequestsController.GetServiceRequest(1);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(1, result.Value.Code);
        }

        [Test]
        public async Task Get_Non_Existent_ServiceRequest()
        {
            ActionResult<ServiceRequestViewModel> result = await _serviceRequestsController.GetServiceRequest(3);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task Get_Existing_Pending_ServiceRequest()
        {
            ActionResult<ServiceRequestViewModel> result = await _serviceRequestsController.PendingServiceRequest("1007870922");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual("1007870922", result.Value.ClientId);
        }

        [Test]
        public async Task Get_Non_Existent_Pending_ServiceRequest()
        {
            ActionResult<ServiceRequestViewModel> result = await _serviceRequestsController.PendingServiceRequest("1007870919");

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task Update_Existing_ServiceRequest()
        {
            ActionResult<ServiceRequestViewModel> result = await _serviceRequestsController.PutServiceRequest(1, new ServiceRequestEditModel
            {
                State = ServiceRequestState.Accepted
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(ServiceRequestState.Accepted, result.Value.State);
        }

        [Test]
        public async Task Update_Non_Existent_ServiceRequest()
        {
            ActionResult<ServiceRequestViewModel> result = await _serviceRequestsController.PutServiceRequest(3, new ServiceRequestEditModel
            {
                State = ServiceRequestState.Accepted
            });

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Save_ServiceRequest()
        {
            ActionResult<ServiceRequestViewModel> result = await _serviceRequestsController.PostServiceRequest(new ServiceRequestInputModel
            {
                ClientId = "1007870919",
                Periodicity = PeriodicityType.Casual,
                ServiceCodes = new List<string>
                {
                    "AERR"
                }
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

    }
}
