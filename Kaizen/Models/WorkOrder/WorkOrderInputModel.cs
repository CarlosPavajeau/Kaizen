using System;

namespace Kaizen.Models.WorkOrder
{
    public class WorkOrderInputModel : WorkOrderEditModel
    {
        public string Sector { get; set; }
        public DateTime ExecutionDate { get; set; }
        public DateTime Validity { get; set; }

        public int ActivityCode { get; set; }
        public string EmployeeId { get; set; }
    }
}
