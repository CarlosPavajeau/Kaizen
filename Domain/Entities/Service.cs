using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class Service
    {
        [Key, MaxLength(15)]
        public string Code { get; set; }
        [MaxLength(40)]
        public string Name { get; set; }
        [ForeignKey("ServiceType")]
        public ServiceType ServiceType { get; set; }
        public decimal Cost { get; set; }
    }
}
