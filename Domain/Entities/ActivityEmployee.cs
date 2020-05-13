using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class ActivityEmployee
    {
        [Required]
        public int ActivityCode { get; set; }
        [MaxLength(10), Required]
        public string EmployeeId { get; set; }

        [ForeignKey("ActivityCode")]
        public Activity Activity { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
