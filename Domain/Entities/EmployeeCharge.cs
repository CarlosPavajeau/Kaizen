using System.ComponentModel.DataAnnotations;

namespace Kaizen.Domain.Entities
{
    public class EmployeeCharge
    {
        public EmployeeCharge()
        {

        }

        public EmployeeCharge(string charge)
        {
            Charge = charge;
        }

        public int Id { get; set; }
        [MaxLength(50)]
        public string Charge { get; set; }
    }
}
