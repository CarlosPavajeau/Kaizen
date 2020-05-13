using System.Collections.Generic;

namespace Kaizen.Domain.Entities
{
    public class ServiceInvoice : Invoice
    {
        public List<ServiceInvoiceDetail> ServiceInvoiceDetails { get; set; }
    }
}
