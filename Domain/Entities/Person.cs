using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class Person : Entity
    {
        [Key, MaxLength(10)]
        public string Id { get; set; }
        [Required, MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(20)]
        public string SecondName { get; set; }
        [Required, MaxLength(20)]
        public string LastName { get; set; }
        [MaxLength(20)]
        public string SecondLastName { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
