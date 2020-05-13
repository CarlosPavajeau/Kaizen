using System.Collections.Generic;

namespace Kaizen.Domain.Entities
{
    public class ProductInvoice : Invoice
    {
        public List<ProductInvoiceDetail> ProductInvoiceDetails { get; set; }
    }
}
