namespace Kaizen.Domain.Entities
{
    public class ProductInvoiceDetail : IInvoiceDetail<Product>
    {
        public Product Detail { get; set; }
    }
}
