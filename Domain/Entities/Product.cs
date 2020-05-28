using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Domain.Entities
{
    public class Product
    {
        [Key]
        [MaxLength(15)]
        public string Code { get; set; }
        [MaxLength(40)]
        public string Name { get; set; }
        public int Amount { get; set; }
        public int ApplicationMonths { get; set; }
        [MaxLength(350)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string Presentation { get; set; }
        public decimal Price { get; set; }

        [MaxLength(50)]
        public string HealthRegister { get; set; }
        [MaxLength(50)]
        public string DataSheet { get; set; }
        [MaxLength(50)]
        public string SafetySheet { get; set; }
        [MaxLength(50)]
        public string EmergencyCard { get; set; }

        public List<ProductService> ProductsServices { get; set; }
    }
}
