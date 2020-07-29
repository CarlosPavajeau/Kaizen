using System.Collections.Generic;

namespace Kaizen.Domain.Entities
{
    public class ServiceInvoice : Invoice
    {
        public List<ServiceInvoiceDetail> ServiceInvoiceDetails { get; set; } = new List<ServiceInvoiceDetail>();

        public void AddDetail(Service service)
        {
            ServiceInvoiceDetails.Add(new ServiceInvoiceDetail
            {
                Detail = service,
                ServiceName = service.Name,
                Total = service.Cost
            });

            SubTotal += service.Cost;
        }
    }
}
