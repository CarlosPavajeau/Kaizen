using System.Collections.Generic;

namespace Kaizen.Domain.Entities
{
    class ProductInvoice : Invoice
    {
        public List<ProductInvoiceDetail> ProductInvoiceDetails { get; set; }
    }
}
