using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.ProductInvoice
{
    public class ProductInvoiceDetailInputModel
    {
        [Required(ErrorMessage = "El código del producto es requerido")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "La cantidad de productos es requerida")]
        public int Amount { get; set; }
    }
}
