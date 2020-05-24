using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class DataSheet
    {
        [MaxLength(40)]
        public string Presentation { get; set; }
        public decimal Price { get; set; }
        [MaxLength(350)]
        public string Description { get; set; }

        [Key, MaxLength(15)]
        public string ProductCode { get; set; }
        [ForeignKey("ProductCode")]
        public Product Product { get; set; }
    }
}
