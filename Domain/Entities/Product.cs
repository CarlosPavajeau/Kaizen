using System.ComponentModel.DataAnnotations;

namespace Kaizen.Domain.Entities
{
    public class Product
    {
        [Key]
        [MaxLength(15)]
        public string Code { get; set; }
        [MaxLength(30)]
        public string HealthRegister { get; set; }
        public int Amount { get; set; }
        public int ApplicationMonths { get; set; }

        public DataSheet DataSheet { get; set; }
        public SafetySheet SafetySheet { get; set; }
        public EmergencyCard EmergencyCard { get; set; }
    }
}
