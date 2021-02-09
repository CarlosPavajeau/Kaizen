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
    [TestFixture, Order(6)]
    public class ProductInvoicesRepositoryTest : BaseRepositoryTest
    {
        private IProductInvoicesRepository _productInvoicesRepository;
        private ApplicationDbContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            DetachAllEntities();

            _productInvoicesRepository = ServiceProvider.GetService<IProductInvoicesRepository>();
            _dbContext = ServiceProvider.GetService<ApplicationDbContext>();
        }

        [Test]
        public void CheckProductInvoicesRepository()
        {
            Assert.IsNotNull(_productInvoicesRepository);
        }

        [Test]
        public async Task Save_Valid_ProductInvoice()
        {
            try
            {
                ProductInvoice productInvoice = new ProductInvoice
                {
                    State = InvoiceState.Generated,
                    ClientId = "12345678",
                };

                productInvoice.CalculateTotal();

                await _productInvoicesRepository.Insert(productInvoice);
                await _dbContext.SaveChangesAsync();

                Assert.Pass();
            }
            catch (DbUpdateException e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public async Task Search_Non_Existent_ProductInvoice()
        {
            ProductInvoice productInvoice = await _productInvoicesRepository.FindByIdAsync(2);

            Assert.IsNull(productInvoice);
        }

        [Test]
        public async Task Search_Existing_ProductInvoice()
        {
            ProductInvoice productInvoice = await _productInvoicesRepository.FindByIdAsync(1);

            Assert.IsNotNull(productInvoice);
            Assert.AreEqual(InvoiceState.Generated, productInvoice.State);
            Assert.AreEqual("12345678", productInvoice.ClientId);
        }

        [Test]
        public async Task Update_Existent_ProductInvoice()
        {
            ProductInvoice productInvoice = await _productInvoicesRepository.FindByIdAsync(1);
            Assert.IsNotNull(productInvoice);

            productInvoice.State = InvoiceState.Paid;
            productInvoice.PaymentMethod = PaymentMethod.Cash;

            _productInvoicesRepository.Update(productInvoice);
            await _dbContext.SaveChangesAsync();

            productInvoice = await _productInvoicesRepository.FindByIdAsync(1);

            Assert.AreEqual(InvoiceState.Paid, productInvoice.State);
            Assert.AreEqual(PaymentMethod.Cash, productInvoice.PaymentMethod);
        }
    }
}
