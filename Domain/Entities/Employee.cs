using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class Employee : Person
    {
        [ForeignKey("EmployeeChargeId")]
        public EmployeeCharge EmployeeCharge { get; set; }
    }
}
