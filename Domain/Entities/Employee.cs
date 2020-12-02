using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class Employee : Person
    {
        public int ChargeId { get; set; }
        [MaxLength(30)]
        public string ContractCode { get; set; }

        [ForeignKey("ChargeId")]
        public EmployeeCharge EmployeeCharge { get; set; }
        [ForeignKey("ContractCode")]
        public EmployeeContract EmployeeContract { get; set; }

        public EmployeeState State { get; set; }

        public List<ActivityEmployee> EmployeesActivities { get; set; }
        public List<EmployeeService> EmployeesServices { get; set; }

        [NotMapped]
        public List<Activity> Activities { get; set; }
        [NotMapped]
        public List<Service> Services { get; set; }
    }
}
