using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class ServiceRequestService
    {
        [Required]
        public int ServiceRequestCode { get; set; }
        [Required, MaxLength(15)]
        public string ServiceCode { get; set; }

        [ForeignKey("ServiceCode")]
        public Service Service { get; set; }
        [ForeignKey("ServiceRequestCode")]
        public ServiceRequest ServiceRequest { get; set; }
    }
}
