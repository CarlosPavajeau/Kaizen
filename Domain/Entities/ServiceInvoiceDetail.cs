using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class ServiceInvoiceDetail : IInvoiceDetail<Service>
    {
        public int Id { get; set; }

        [ForeignKey("ServiceCode")]
        public Service Detail { get; set; }

        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public decimal Total { get; set; }
    }
}
