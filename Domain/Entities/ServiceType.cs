using System.ComponentModel.DataAnnotations;

namespace Kaizen.Domain.Entities
{
    public class ServiceType
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(70)]
        public string Name { get; set; }
    }
}
