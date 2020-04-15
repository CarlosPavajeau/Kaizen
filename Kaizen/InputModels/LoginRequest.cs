using System.ComponentModel.DataAnnotations;

namespace Kaizen.InputModels
{
    public class LoginRequest
    {
        [Required]
        public string UsernameOrEmail { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
