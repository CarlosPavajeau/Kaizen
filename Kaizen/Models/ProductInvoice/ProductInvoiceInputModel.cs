using System.Collections.Generic;

namespace Kaizen.Models.ProductInvoice
{
    public class ProductInvoiceInputModel : ProductInvoiceEditModel
    {
        public string ClientId { get; set; }

        public List<ProductInvoiceDetailInputModel> ProductInvoiceDetails { get; set; }
    }
}
