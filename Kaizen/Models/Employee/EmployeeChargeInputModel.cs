using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.Employee
{
    public class EmployeeChargeInputModel
    {
        [Required(ErrorMessage = "Nombre del cargo del empleado requerido")]
        [MaxLength(50, ErrorMessage = "El nombre del cargo del empleado no debe exceder los 50 caracteres")]
        public string Charge { get; set; }
    }
}
