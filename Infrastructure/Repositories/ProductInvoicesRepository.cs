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
                .Where(s => s.State == InvoiceState.Generated && (DateTime.Now - s.GenerationDate).Days >= Invoice.DayLimits)
                .ToListAsync();
        }

        async Task IProductInvoicesRepository.Insert(ProductInvoice entity)
        {
            entity.GenerationDate = DateTime.Now;

            List<ProductInvoiceDetail> productInvoiceDetails = entity.ProductInvoiceDetails
                .Select(s => new ProductInvoiceDetail { ProductCode = s.ProductCode, Amount = s.Amount })
                .ToList();
            List<string> productCodes = entity.ProductInvoiceDetails.Select(p => p.ProductCode).ToList();
            List<Product> products = await ApplicationDbContext.Products
                .Where(p => productCodes.Contains(p.Code)).ToListAsync();

            entity.ProductInvoiceDetails.Clear();

            productCodes.ForEach(productCode =>
            {
                Product product = products.Where(p => p.Code == productCode).FirstOrDefault();
                int amount = productInvoiceDetails.Where(detail => detail.ProductCode == productCode).FirstOrDefault().Amount;
                entity.AddDetail(product, amount);
            });

            entity.CalculateTotal();

            base.Insert(entity);
        }
    }
}
