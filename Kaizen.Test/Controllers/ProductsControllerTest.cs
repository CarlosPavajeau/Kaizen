using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.Product;
using Kaizen.Test.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Kaizen.Test.Controllers
{
    [TestFixture]
    public class ProductsControllerTest : BaseControllerTest
    {
        private ProductsController _productsController;
        private Mock<IProductsRepository> _productsRepository;
        private Mock<IUnitWork> _unitWork;

        [SetUp]
        public void SetUp()
        {
            _productsRepository = new Mock<IProductsRepository>();
            _unitWork = new Mock<IUnitWork>();

            _productsController = new ProductsController(_productsRepository.Object, _unitWork.Object,
                ServiceProvider.GetService<IMapper>());
        }

        [Test]
        public async Task GetProducts()
        {
            _productsRepository.Setup(r => r.GetAll()).Returns(new TestAsyncEnumerable<Product>(new List<Product>
            {
                new()
                {
                    Code = "123",
                    Name = "Pesticida",
                    Amount = 30
                },
                new()
                {
                    Code = "321",
                    Name = "Pesticida de insectos",
                    Amount = 50
                }
            }).AsQueryable());

            OkObjectResult result = (await _productsController.GetProducts()).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }

        [Test]
        public async Task Get_Existing_Product()
        {
            _productsRepository.Setup(r => r.FindByIdAsync("123")).ReturnsAsync(new Product
            {
                Code = "123",
                Name = "Pesticida"
            });

            ActionResult<ProductViewModel> result = await _productsController.GetProduct("123");

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }

        [Test]
        public async Task Get_Non_Existent_Product()
        {
            _productsRepository.Setup(r => r.FindByIdAsync("321")).ReturnsAsync((Product)null);

            ActionResult<ProductViewModel> result = await _productsController.GetProduct("321");

            Assert.NotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task Check_Product_Exists()
        {
            _productsRepository.Setup(r => r.GetAll()).Returns(new TestAsyncEnumerable<Product>(new List<Product>
            {
                new()
                {
                    Code = "123",
                    Name = "Pesticida",
                    Amount = 30
                },
                new()
                {
                    Code = "321",
                    Name = "Pesticida de insectos",
                    Amount = 50
                }
            }).AsQueryable());

            bool result = await _productsController.CheckExists("123");

            Assert.NotNull(result);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task Update_Existing_Product()
        {
            _productsRepository.Setup(r => r.FindByIdAsync("123")).ReturnsAsync(new Product
            {
                Code = "123",
                Name = "Pesticida"
            });
            _productsRepository.Setup(r => r.Update(It.IsAny<Product>())).Verifiable();
            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            ActionResult<ProductViewModel> result = await _productsController.PutProduct("123", new ProductEditModel
            {
                Name = "Pesticida de insectos"
            });

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.AreEqual("Pesticida de insectos", result.Value.Name);
        }

        [Test]
        public async Task Update_Non_Existent_Product()
        {
            _productsRepository.Setup(r => r.FindByIdAsync("321")).ReturnsAsync((Product)null);

            ActionResult<ProductViewModel> result = await _productsController.PutProduct("123", new ProductEditModel
            {
                Name = "Pesticida de insectos"
            });

            Assert.NotNull(result);
            Assert.Null(result.Value);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Save_New_Product()
        {
            _productsRepository.Setup(r => r.Insert(It.IsAny<Product>())).Verifiable();
            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            ActionResult<ProductViewModel> result = await _productsController.PostProduct(new ProductInputModel
            {
                Code = "123",
                Name = "Pesticida",
                Amount = 3
            });

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }

        [Test]
        public async Task Save_Existing_Product()
        {
            _productsRepository.Setup(r => r.GetAll()).Returns(new TestAsyncEnumerable<Product>(new List<Product>
            {
                new()
                {
                    Code = "123",
                    Name = "Pesticida",
                    Amount = 30
                },
                new()
                {
                    Code = "321",
                    Name = "Pesticida de insectos",
                    Amount = 50
                }
            }).AsQueryable());

            _productsRepository.Setup(r => r.Insert(It.IsAny<Product>())).Verifiable();
            _unitWork.Setup(r => r.SaveAsync()).Throws(new DbUpdateException());

            ActionResult<ProductViewModel> result = await _productsController.PostProduct(new ProductInputModel
            {
                Code = "321",
                Name = "Pesticida",
                Amount = 3
            });

            Assert.NotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<ConflictObjectResult>(result.Result);
        }
    }
}
