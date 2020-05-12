using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class EmployeesServices
    {
        [MaxLength(10)]
        public string EmployeeId { get; set; }
        [MaxLength(15)]
        public string ServiceCode { get; set; }

        [ForeignKey("ServiceCode")]
        public Service Service { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
