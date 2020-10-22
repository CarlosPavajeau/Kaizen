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
        private IProductsRepository productsRepository;
        private ApplicationDbContext dbContext;

        [SetUp]
        public void SetUp()
        {
            DetachAllEntities();

            productsRepository = ServiceProvider.GetService<IProductsRepository>();
            dbContext = ServiceProvider.GetService<ApplicationDbContext>();
        }

        [Test]
        public void CheckProductsRepository()
        {
            Assert.IsNotNull(productsRepository);
        }

        [Test]
        public async Task SaveProduct()
        {
            try
            {
                Product product = new Product
                {
                    Code = "123ER",
                    Name = "Pesticida",
                    Amount = 10,
                    ApplicationMonths = 10,
                    Description = "Pesticida de plagas",
                    Presentation = "1Litro",
                    Price = 50000,
                    HealthRegister = "HealthRegister.pdf",
                    DataSheet = "DataSheet.pdf",
                    SafetySheet = "SafetySheet.pdf",
                    EmergencyCard = "EmergencyCard.pdf",
                };

                productsRepository.Insert(product);

                await dbContext.SaveChangesAsync();

                Assert.Pass();
            }
            catch (DbUpdateException)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task SearchProduct()
        {
            try
            {
                Product product = await productsRepository.FindByIdAsync("123ER");

                Assert.IsNotNull(product);
                Assert.AreEqual("123ER", product.Code);
                Assert.AreEqual("Pesticida", product.Name);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task UpdateProduct()
        {
            try
            {
                Product product = await productsRepository.FindByIdAsync("123ER");
                Assert.IsNotNull(product);

                product.Description = "Pesticida de plagas voladoras";
                productsRepository.Update(product);

                await dbContext.SaveChangesAsync();

                product = await productsRepository.FindByIdAsync("123ER");
                Assert.AreEqual("Pesticida de plagas voladoras", product.Description);
            }
            catch (DbUpdateException)
            {
                Assert.Fail();
            }
        }
    }
}
