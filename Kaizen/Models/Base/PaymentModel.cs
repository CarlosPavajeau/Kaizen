using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.Base
{
    public class PaymentModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string PaymentMethodId { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
