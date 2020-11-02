using Kaizen.Models.Activity;
using Kaizen.Models.Employee;

namespace Kaizen.Models.WorkOrder
{
    public class WorkOrderViewModel : WorkOrderInputModel
    {
        public int Code { get; set; }

        public EmployeeViewModel Employee { get; set; }
        public ActivityViewModel Activity { get; set; }
        public SectorViewModel Sector { get; set; }
    }
}
