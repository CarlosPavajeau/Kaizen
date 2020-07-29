using System.Collections.Generic;
using System.Linq;

namespace Kaizen.Domain.Entities
{
    public class ProductInvoice : Invoice
    {
        public List<ProductInvoiceDetail> ProductInvoiceDetails { get; set; } = new List<ProductInvoiceDetail>();

        public void AddDetail(Product product, int amount)
        {
            ProductInvoiceDetails.Add(new ProductInvoiceDetail
            {
                Detail = product,
                ProductName = product.Name,
                Amount = amount,
                Total = product.Price * amount
            });

            SubTotal += ProductInvoiceDetails.Last().Total;
        }
    }
}
