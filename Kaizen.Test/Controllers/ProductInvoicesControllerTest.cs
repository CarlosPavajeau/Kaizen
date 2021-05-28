using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.ProductInvoice;
using Kaizen.Test.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Kaizen.Test.Controllers
{
    [TestFixture]
    public class ProductInvoicesControllerTest : BaseControllerTest
    {
        private ProductInvoicesController _productInvoicesController;
        private Mock<IProductInvoicesRepository> _productInvoicesRepository;
        private Mock<IUnitWork> _unitWork;

        [SetUp]
        public void SetUp()
        {
            _productInvoicesRepository = new Mock<IProductInvoicesRepository>();
            _unitWork = new Mock<IUnitWork>();

            _productInvoicesController = new ProductInvoicesController(_productInvoicesRepository.Object,
                _unitWork.Object, ServiceProvider.GetService<IMapper>());

            SetUpProductInvoicesRepository();
            SetUpUnitWork();
        }

        private void SetUpProductInvoicesRepository()
        {
            _productInvoicesRepository.Setup(r => r.GetAll()).Returns(new TestAsyncEnumerable<ProductInvoice>(
                new List<ProductInvoice>
                {
                    new()
                    {
                        Id = 1,
                        ClientId = "1007870922",
                        GenerationDate = DateTime.Now,
                        ProductInvoiceDetails = new List<ProductInvoiceDetail>()
                    },
                    new()
                    {
                        Id = 2,
                        ClientId = "1007870922",
                        GenerationDate = DateTime.Now,
                        ProductInvoiceDetails = new List<ProductInvoiceDetail>()
                    }
                }).AsQueryable());

            _productInvoicesRepository.Setup(r => r.FindByIdAsync(1)).ReturnsAsync(new ProductInvoice
            {
                Id = 1,
                ClientId = "1007870922",
                GenerationDate = DateTime.Now,
                ProductInvoiceDetails = new List<ProductInvoiceDetail>()
            });
            _productInvoicesRepository.Setup(r => r.FindByIdAsync(3)).ReturnsAsync((ProductInvoice) null);

            _productInvoicesRepository.Setup(r => r.Update(It.IsAny<ProductInvoice>()));
            _productInvoicesRepository.Setup(r => r.Insert(It.IsAny<ProductInvoice>())).Verifiable();
        }

        private void SetUpUnitWork()
        {
            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);
        }

        [Test]
        public async Task Get_All_ProductInvoices()
        {
            OkObjectResult result = (await _productInvoicesController.GetProductInvoices()).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<ProductInvoiceViewModel>>(result.Value);
        }

        [Test]
        public async Task Get_Existing_ProductInvoice()
        {
            ActionResult<ProductInvoiceViewModel> result = await _productInvoicesController.GetProductInvoice(1);

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }

        [Test]
        public async Task Get_Non_Existent_ProductInvoice()
        {
            ActionResult<ProductInvoiceViewModel> result = await _productInvoicesController.GetProductInvoice(3);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task Update_Existing_ProductInvoice()
        {
            ActionResult<ProductInvoiceViewModel> result = await _productInvoicesController.PutProductInvoice(1,
                new ProductInvoiceEditModel
                {
                    State = InvoiceState.Regenerated
                });

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }

        [Test]
        public async Task Update_Non_Existent_ProductInvoice()
        {
            ActionResult<ProductInvoiceViewModel> result = await _productInvoicesController.PutProductInvoice(3,
                new ProductInvoiceEditModel
                {
                    State = InvoiceState.Regenerated
                });

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Save_New_ProductInvoice()
        {
            ActionResult<ProductInvoiceViewModel> result = await _productInvoicesController.PostProductInvoice(
                new ProductInvoiceInputModel
                {
                    ClientId = "1007870922",
                    State = InvoiceState.Generated
                });

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }
    }
}
