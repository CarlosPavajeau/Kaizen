using System;

namespace Kaizen.Models.WorkOrder
{
    public class WorkOrderInputModel : WorkOrderEditModel
    {
        public int SectorId { get; set; }
        public DateTime ExecutionDate { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int ActivityCode { get; set; }
        public string EmployeeId { get; set; }
    }
}
