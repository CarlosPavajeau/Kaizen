using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.Employee
{
    public class EmployeeInputModel : EmployeeEditModel
    {
        [Required(ErrorMessage = "La identificación es requerida")]
        public string Id { get; set; }

        [Required(ErrorMessage = "El id del usuario es requerido")]
        public string UserId { get; set; }
    }
}
