using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class Activity : ServiceRequest
    {
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
