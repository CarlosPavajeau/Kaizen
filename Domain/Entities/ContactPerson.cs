using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class ContactPerson
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(10)]
        public string PhoneNumber { get; set; }
        [Required, MaxLength(10)]
        public string ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }
    }
}
