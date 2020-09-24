using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.ApplicationUser
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "El usuario o email de acceso son requeridos")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "La contraseña de acceso es requerida")]
        public string Password { get; set; }
    }
}
