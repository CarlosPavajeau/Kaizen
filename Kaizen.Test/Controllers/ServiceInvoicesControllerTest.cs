using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.ServiceInvoice;
using Kaizen.Test.Helpers;
using MercadoPago.Client.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Kaizen.Test.Controllers
{
    [TestFixture]
    public class ServiceInvoicesControllerTest : BaseControllerTest
    {
        private ServiceInvoicesController _serviceInvoicesController;
        private Mock<IServiceInvoicesRepository> _serviceInvoicesRepository;
        private Mock<IUnitWork> _unitWork;
        private Mock<PaymentClient> _paymentClient;

        [SetUp]
        public void SetUp()
        {
            _serviceInvoicesRepository = new Mock<IServiceInvoicesRepository>();
            _unitWork = new Mock<IUnitWork>();
            _paymentClient = new Mock<PaymentClient>();

            _serviceInvoicesController = new ServiceInvoicesController(_serviceInvoicesRepository.Object,
                _unitWork.Object, ServiceProvider.GetService<IMapper>(), _paymentClient.Object);

            SetUpServiceInvoicesRepository();
            SetUpUnitWork();
        }

        private void SetUpServiceInvoicesRepository()
        {
            _serviceInvoicesRepository.Setup(r => r.GetAll()).Returns(new TestAsyncEnumerable<ServiceInvoice>(
                new List<ServiceInvoice>
                {
                    new()
                    {
                        Id = 1,
                        ClientId = "1007870922",
                        Client = new Client
                        {
                            Id = "1007870922"
                        }
                    },
                    new()
                    {
                        Id = 2,
                        ClientId = "1007870921",
                        Client = new Client
                        {
                            Id = "1007870921"
                        }
                    }
                }));

            _serviceInvoicesRepository.Setup(r => r.FindByIdAsync(1)).ReturnsAsync(new ServiceInvoice
            {
                Id = 1,
                ClientId = "1007870922",
                Client = new Client
                {
                    Id = "1007870922"
                }
            });

            _serviceInvoicesRepository.Setup(r => r.FindByIdAsync(3)).ReturnsAsync((ServiceInvoice)null);

            _serviceInvoicesRepository.Setup(r => r.GetClientInvoices("1007870922"))
                .ReturnsAsync(new List<ServiceInvoice>
                {
                    new()
                    {
                        Id = 1,
                        ClientId = "1007870922",
                        Client = new Client
                        {
                            Id = "1007870922"
                        }
                    }
                });

            _serviceInvoicesRepository.Setup(r => r.Update(It.IsAny<ServiceInvoice>())).Verifiable();
        }

        private void SetUpUnitWork()
        {
            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);
        }

        [Test]
        public async Task Get_ServiceInvoices()
        {
            OkObjectResult result = (await _serviceInvoicesController.GetServiceInvoices()).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<ServiceInvoiceViewModel>>(result.Value);
        }

        [Test]
        public async Task Get_Existing_ServiceInvoice()
        {
            ActionResult<ServiceInvoiceViewModel> result = await _serviceInvoicesController.GetServiceInvoice(1);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(1, result.Value.Id);
        }

        [Test]
        public async Task Get_Non_Existent_ServiceInvoice()
        {
            ActionResult<ServiceInvoiceViewModel> result = await _serviceInvoicesController.GetServiceInvoice(3);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task Get_ClientInvoices()
        {
            OkObjectResult result = (await _serviceInvoicesController.ClientInvoices("1007870922")).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<ServiceInvoiceViewModel>>(result.Value);
        }

        [Test]
        public async Task Update_Existing_ServiceInvoice()
        {
            ActionResult<ServiceInvoiceViewModel> result = await _serviceInvoicesController.PutServiceInvoice(1, new ServiceInvoiceEditModel
            {
                PaymentMethod = PaymentMethod.Cash,
                State = InvoiceState.Paid
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(PaymentMethod.Cash, result.Value.PaymentMethod);
        }

        [Test]
        public async Task Update_Non_Existent_ServiceInvoice()
        {
            ActionResult<ServiceInvoiceViewModel> result = await _serviceInvoicesController.PutServiceInvoice(3, new ServiceInvoiceEditModel
            {
                PaymentMethod = PaymentMethod.Cash,
                State = InvoiceState.Paid
            });

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }
    }
}
