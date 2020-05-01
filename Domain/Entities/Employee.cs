using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class Employee : Person
    {
		public string ChargeId { get; set; }

		[ForeignKey("ChargeId")]
        public EmployeeCharge EmployeeCharge { get; set; }
    }
}
