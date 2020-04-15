using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.InputModels
{
    public class LoginRequest
    {
        [Required]
        public string UsernameOrEmail { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
