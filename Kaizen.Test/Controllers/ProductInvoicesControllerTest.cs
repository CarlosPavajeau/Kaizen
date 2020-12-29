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
        }

        [Test]
        public async Task GetProductInvoices()
        {
            _productInvoicesRepository.Setup(r => r.GetAll()).Returns(new TestAsyncEnumerable<ProductInvoice>(
                new List<ProductInvoice>
                {
                    new ()
                    {
                        Id = 1,
                        ClientId = "1007870922",
                        GenerationDate = DateTime.Now,
                        ProductInvoiceDetails = new List<ProductInvoiceDetail>()
                    },
                    new ()
                    {
                        Id = 2,
                        ClientId = "1007870922",
                        GenerationDate = DateTime.Now,
                        ProductInvoiceDetails = new List<ProductInvoiceDetail>()
                    }
                }).AsQueryable());

            OkObjectResult result = (await _productInvoicesController.GetProductInvoices()).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<ProductInvoiceViewModel>>(result.Value);
        }

        [Test]
        public async Task GetProductInvoice()
        {
            _productInvoicesRepository.Setup(r => r.FindByIdAsync(1)).ReturnsAsync(new ProductInvoice
            {
                Id = 1,
                ClientId = "1007870922",
                GenerationDate = DateTime.Now,
                ProductInvoiceDetails = new List<ProductInvoiceDetail>()
            });

            ActionResult<ProductInvoiceViewModel> result = await _productInvoicesController.GetProductInvoice(1);

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }

        [Test]
        public async Task PutProductInvoice()
        {
            _productInvoicesRepository.Setup(r => r.FindByIdAsync(1)).ReturnsAsync(new ProductInvoice
            {
                Id = 1,
                ClientId = "1007870922",
                GenerationDate = DateTime.Now,
                ProductInvoiceDetails = new List<ProductInvoiceDetail>()
            });
            _productInvoicesRepository.Setup(r => r.Update(It.IsAny<ProductInvoice>()));
            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);


            ActionResult<ProductInvoiceViewModel> result = await _productInvoicesController.PutProductInvoice(1, new ProductInvoiceEditModel
            {
                State = InvoiceState.Regenerated
            });

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }

        [Test]
        public async Task PostProductInvoice()
        {
            _productInvoicesRepository.Setup(r => r.Insert(It.IsAny<ProductInvoice>())).Verifiable();
            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            var result = await _productInvoicesController.PostProductInvoice(new ProductInvoiceInputModel
            {
                ClientId = "1007870922",
                State = InvoiceState.Generated
            });

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }
    }
}
