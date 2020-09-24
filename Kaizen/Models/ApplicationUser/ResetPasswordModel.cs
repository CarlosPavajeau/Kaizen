using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.ApplicationUser
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "El token de cambio de contraseña es obligatorio")]
        public string Token { get; set; }

        [Required(ErrorMessage = "La nueva contraseña es requerida")]
        public string NewPassword { get; set; }
    }
}
