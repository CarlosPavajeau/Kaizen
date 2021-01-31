using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Repositories
{
    public class ProductInvoicesRepository : RepositoryBase<ProductInvoice, int>, IProductInvoicesRepository
    {
        public ProductInvoicesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<IEnumerable<ProductInvoice>> GetPendingExpiredProductInvoices()
        {
            return await GetAll().Include(s => s.Client).Include(s => s.ProductInvoiceDetails)
                .Where(s => s.State == InvoiceState.Generated &&
                            MySqlDbFunctionsExtensions.DateDiffDay(EF.Functions, DateTime.Now, s.GenerationDate) >=
                            Invoice.DayLimits)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductInvoice>> GetClientInvoices(string clientId)
        {
            return await GetAll()
                .Include(s => s.ProductInvoiceDetails)
                .Include(s => s.Client)
                .Where(s => s.ClientId == clientId)
                .ToListAsync();
        }

        async Task IProductInvoicesRepository.Insert(ProductInvoice entity)
        {
            entity.GenerationDate = DateTime.Now;

            List<ProductInvoiceDetail> productInvoiceDetails = entity.ProductInvoiceDetails
                .Select(s => new ProductInvoiceDetail {ProductCode = s.ProductCode, Amount = s.Amount})
                .ToList();
            List<string> productCodes = entity.ProductInvoiceDetails.Select(p => p.ProductCode).ToList();
            List<Product> products = await ApplicationDbContext.Products
                .Where(p => productCodes.Contains(p.Code)).ToListAsync();

            entity.ProductInvoiceDetails.Clear();

            productCodes.ForEach(productCode =>
            {
                Product product = products.FirstOrDefault(p => p.Code == productCode);
                int amount = productInvoiceDetails.FirstOrDefault(detail => detail.ProductCode == productCode).Amount;
                entity.AddDetail(product, amount);
            });

            entity.CalculateTotal();

            base.Insert(entity);
        }

        public override async Task<ProductInvoice> FindByIdAsync(int id)
        {
            return await ApplicationDbContext.ProductInvoices.Include(p => p.Client)
                .Include(p => p.ProductInvoiceDetails)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
