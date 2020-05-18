using System.Collections.Generic;
using Kaizen.Models.Employee;
using Kaizen.Models.Service;

namespace Kaizen.Models.Activity
{
    public class ActivityViewModel : ActivityInputModel
    {
        public List<EmployeeViewModel> Employees { get; set; }
        public List<ServiceViewModel> Services { get; set; }
    }
}
