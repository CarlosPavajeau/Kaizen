using System.Collections.Generic;
using Kaizen.Models.Client;

namespace Kaizen.Models.ProductInvoice
{
    public class ProductInvoiceViewModel : ProductInvoiceEditModel
    {
        public string ClientId { get; set; }

        public ClientViewModel Client { get; set; }
        public List<ProductInvoiceDetailViewModel> ProductInvoiceDetails { get; set; }

        public decimal IVA { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
