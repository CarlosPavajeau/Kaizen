using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class Employee : Person
    {
		public int ChargeId { get; set; }

		[ForeignKey("ChargeId")]
        public EmployeeCharge EmployeeCharge { get; set; }
    }
}
