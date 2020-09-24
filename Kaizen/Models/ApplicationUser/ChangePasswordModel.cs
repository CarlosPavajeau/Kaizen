using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.ApplicationUser
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "La antigua contraseña de acceso es requerida")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "La nueva contraseña de acceso es requerida")]
        public string NewPassword { get; set; }
    }
}
