using System;
using Kaizen.Models.WorkOrder;

namespace Kaizen.Models.Certificate
{
    public class CertificateViewModel
    {
        public int Id { get; set; }

        public DateTime Validity { get; set; }

        public WorkOrderViewModel WorkOrder { get; set; }
    }
}
