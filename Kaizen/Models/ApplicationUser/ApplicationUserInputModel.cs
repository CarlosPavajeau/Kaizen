using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.ApplicationUser
{
    public class ApplicationUserInputModel : ApplicationUserEditModel
    {
        [Required(ErrorMessage = "Nombre de usuario requerido")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Rol de usuario requerido")]
        public string Role { get; set; }
    }
}
