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
    [Order(3)]
    public class ProductsRepositoryTest : BaseRepositoryTest
    {
        private IProductsRepository _productsRepository;
        private ApplicationDbContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            DetachAllEntities();

            _productsRepository = ServiceProvider.GetService<IProductsRepository>();
            _dbContext = ServiceProvider.GetService<ApplicationDbContext>();
        }

        [Test]
        public void CheckProductsRepository()
        {
            Assert.IsNotNull(_productsRepository);
        }

        [Test]
        public async Task Save_Invalid_Product()
        {
            try
            {
                Product product = new Product();

                _productsRepository.Insert(product);

                await _dbContext.SaveChangesAsync();

                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task Save_Valid_Product()
        {
            try
            {
                Product product = new Product
                {
                    Code = "123ER",
                    Name = "Pesticida",
                    Amount = 10,
                    ApplicationMonths =
                        ApplicationMonths.January | ApplicationMonths.February | ApplicationMonths.March,
                    Description = "Pesticida de plagas",
                    Presentation = "1Litro",
                    Price = 50000,
                    HealthRegister = "HealthRegister.pdf",
                    DataSheet = "DataSheet.pdf",
                    SafetySheet = "SafetySheet.pdf",
                    EmergencyCard = "EmergencyCard.pdf",
                };

                _productsRepository.Insert(product);

                await _dbContext.SaveChangesAsync();

                Assert.Pass();
            }
            catch (DbUpdateException e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public async Task Search_Non_Existing_Product()
        {
            Product product = await _productsRepository.FindByIdAsync("123EE");

            Assert.IsNull(product);
        }

        [Test]
        public async Task Search_Existing_Product()
        {
            Product product = await _productsRepository.FindByIdAsync("123ER");

            Assert.IsNotNull(product);
            Assert.AreEqual("123ER", product.Code);
            Assert.AreEqual("Pesticida", product.Name);
        }

        [Test]
        public async Task Update_Existing_Product()
        {
            try
            {
                Product product = await _productsRepository.FindByIdAsync("123ER");
                Assert.IsNotNull(product);

                product.Description = "Pesticida de plagas voladoras";
                _productsRepository.Update(product);

                await _dbContext.SaveChangesAsync();

                product = await _productsRepository.FindByIdAsync("123ER");
                Assert.AreEqual("Pesticida de plagas voladoras", product.Description);
            }
            catch (DbUpdateException e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
