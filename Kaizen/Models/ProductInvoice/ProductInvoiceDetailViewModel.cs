using Kaizen.Models.Product;

namespace Kaizen.Models.ProductInvoice
{
    public class ProductInvoiceDetailViewModel : ProductInvoiceDetailInputModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public ProductViewModel Detail { get; set; }
        public decimal Total { get; set; }
    }
}
