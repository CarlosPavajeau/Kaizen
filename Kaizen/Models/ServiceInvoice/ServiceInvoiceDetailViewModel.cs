using Kaizen.Models.Service;

namespace Kaizen.Models.ServiceInvoice
{
    public class ServiceInvoiceDetailViewModel
    {
        public int Id { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public ServiceViewModel Service { get; set; }
        public decimal Total { get; set; }
    }
}
