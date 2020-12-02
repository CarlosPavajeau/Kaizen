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
        private IProductInvoicesRepository productInvoicesRepository;
        private ApplicationDbContext dbContext;

        [SetUp]
        public void SetUp()
        {
            productInvoicesRepository = ServiceProvider.GetService<IProductInvoicesRepository>();
            dbContext = ServiceProvider.GetService<ApplicationDbContext>();
        }

        [Test]
        public void CheckProductInvoicesRepository()
        {
            Assert.IsNotNull(productInvoicesRepository);
        }

        [Test]
        public async Task SaveProductInvoice()
        {
            try
            {
                ProductInvoice productInvoice = new ProductInvoice
                {
                    State = InvoiceState.Generated,
                    ClientId = "12345678",
                };

                productInvoice.CalculateTotal();

                await productInvoicesRepository.Insert(productInvoice);
                await dbContext.SaveChangesAsync();

                Assert.Pass();
            }
            catch (DbUpdateException)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task SearchProductInvoice()
        {
            ProductInvoice productInvoice = await productInvoicesRepository.FindByIdAsync(1);

            Assert.IsNotNull(productInvoice);
            Assert.AreEqual(InvoiceState.Generated, productInvoice.State);
            Assert.AreEqual("12345678", productInvoice.ClientId);
        }

        [Test]
        public async Task UpdateProductInvoice()
        {
            ProductInvoice productInvoice = await productInvoicesRepository.FindByIdAsync(1);
            Assert.IsNotNull(productInvoice);

            productInvoice.State = InvoiceState.Paid;
            productInvoice.PaymentMethod = PaymentMethod.Cash;

            productInvoicesRepository.Update(productInvoice);
            await dbContext.SaveChangesAsync();

            productInvoice = await productInvoicesRepository.FindByIdAsync(1);

            Assert.AreEqual(InvoiceState.Paid, productInvoice.State);
            Assert.AreEqual(PaymentMethod.Cash, productInvoice.PaymentMethod);
        }
    }
}
