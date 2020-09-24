using System.ComponentModel.DataAnnotations;
using Kaizen.Domain.Entities;
using Kaizen.Models.Base;

namespace Kaizen.Models.Employee
{
    public class EmployeeEditModel : PersonEditModel
    {
        [Required(ErrorMessage = "El código del cargo del empleado es requerido")]
        public int ChargeId { get; set; }

        [Required(ErrorMessage = "El código del contrato del empleado es requerido")]
        public string ContractCode { get; set; }

        public EmployeeContractModel EmployeeContract { get; set; }

        [Required(ErrorMessage = "El estado del empleado es requerido")]
        public EmployeeState State { get; set; }
    }
}
