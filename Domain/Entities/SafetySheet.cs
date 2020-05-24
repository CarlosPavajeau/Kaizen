using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class SafetySheet
    {
        [Key, MaxLength(15)]
        public string ProductCode { get; set; }
        [ForeignKey("ProductCode")]
        public Product Product { get; set; }
    }
}
