using System.Collections.Generic;

namespace Kaizen.Domain.Entities
{
    class ServiceInvoice : Invoice
    {
        public List<ServiceInvoiceDetail> ServiceInvoiceDetails { get; set; }
    }
}
