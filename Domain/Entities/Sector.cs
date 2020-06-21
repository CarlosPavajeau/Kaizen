using System.ComponentModel.DataAnnotations;

namespace Kaizen.Domain.Entities
{
    public class Sector
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(40)]
        public string Name { get; set; }
    }
}
