using System;
using System.Collections.Generic;
using Kaizen.Models.Client;

namespace Kaizen.Models.ServiceInvoice
{
    public class ServiceInvoiceViewModel : ServiceInvoiceEditModel
    {
        public int Id { get; set; }

        public List<ServiceInvoiceDetailViewModel> ServiceInvoiceDetails { get; set; }
        public ClientViewModel Client { get; set; }

        public DateTime GenerationDate { get; set; }

        public decimal IVA { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
