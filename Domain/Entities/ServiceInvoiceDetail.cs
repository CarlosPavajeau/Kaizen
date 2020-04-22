namespace Kaizen.Domain.Entities
{
    public class ServiceInvoiceDetail : IInvoiceDetail<Service>
    {
        public Service Detail { get; set; }
    }
}
