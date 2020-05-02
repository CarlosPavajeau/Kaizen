using System.ComponentModel.DataAnnotations;

namespace Kaizen.Domain.Entities
{
    public class ServiceType
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
