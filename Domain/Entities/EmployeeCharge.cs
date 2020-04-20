using System.ComponentModel.DataAnnotations;

namespace Kaizen.Domain.Entities
{
    public class EmployeeCharge
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Charge { get; set; }
    }
}
