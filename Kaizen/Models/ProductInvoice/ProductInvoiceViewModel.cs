using System;
using System.Collections.Generic;
using Kaizen.Models.Client;

namespace Kaizen.Models.ProductInvoice
{
    public class ProductInvoiceViewModel : ProductInvoiceEditModel
    {
        public int Id { get; set; }
        public string ClientId { get; set; }

        public ClientViewModel Client { get; set; }
        public List<ProductInvoiceDetailViewModel> ProductInvoiceDetails { get; set; }

        public DateTime GenerationDate { get; set; }

        public decimal IVA { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
