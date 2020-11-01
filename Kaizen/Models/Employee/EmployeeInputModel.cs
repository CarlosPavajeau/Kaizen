using System.ComponentModel.DataAnnotations;
using Kaizen.Models.ApplicationUser;

namespace Kaizen.Models.Employee
{
    public class EmployeeInputModel : EmployeeEditModel
    {
        [Required(ErrorMessage = "La identificación es requerida")]
        public string Id { get; set; }

        [Required(ErrorMessage = "El usuario es requerido")]
        public ApplicationUserInputModel User { get; set; }
    }
}
