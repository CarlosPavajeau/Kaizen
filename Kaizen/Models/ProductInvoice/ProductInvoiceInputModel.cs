using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.ProductInvoice
{
    public class ProductInvoiceInputModel : ProductInvoiceEditModel
    {
        [Required(ErrorMessage = "La identificación del cliente es requerida")]
        public string ClientId { get; set; }

        public List<ProductInvoiceDetailInputModel> ProductInvoiceDetails { get; set; }
    }
}
