using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class ProductInvoiceDetail : IInvoiceDetail<Product>
    {
        public int Id { get; set; }

        [ForeignKey("ProductCode")]
        public Product Detail { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }

        public int Amount { get; set; }
        public decimal Total { get; set; }
    }
}
