using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kaizen.Validations;

namespace Kaizen.Models.ProductInvoice
{
    public class ProductInvoiceInputModel : ProductInvoiceEditModel
    {
        [Required(ErrorMessage = "La identificación del cliente es requerida")]
        public string ClientId { get; set; }

        [NotNullOrEmptyCollection(ErrorMessage = "Se deben asignar los detalles de la factura de productos")]
        public List<ProductInvoiceDetailInputModel> ProductInvoiceDetails { get; set; }
    }
}
