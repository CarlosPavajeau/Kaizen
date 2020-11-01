using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.ApplicationUser
{
    public class ApplicationUserInputModel
    {
        [Required(ErrorMessage = "Nombre de usuario requerido")]
        public string Username { get; set; }
        [Required(ErrorMessage = "La contraseña de acceso es requerida")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El email de acceso es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El número de teléfono es requerido")]
        public string PhoneNumber { get; set; }
    }
}
