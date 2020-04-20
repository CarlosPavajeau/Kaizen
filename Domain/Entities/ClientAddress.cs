using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class ClientAddress
    {
        [MaxLength(40)]
        public string City { get; set; }
        [MaxLength(40)]
        public string Neighborhood { get; set; }
        [MaxLength(40)]
        public string Street { get; set; }

        [Key, MaxLength(10)]
        public string ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
    }
}
