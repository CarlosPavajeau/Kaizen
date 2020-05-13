using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class ProductService
    {
        [MaxLength(15)]
        public string ProductCode { get; set; }
        [MaxLength(15)]
        public string ServiceCode { get; set; }

        [ForeignKey("ServiceCode")]
        public Service Service { get; set; }
        [ForeignKey("ProductCode")]
        public Product Product { get; set; }
    }
}
