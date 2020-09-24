using System;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.Employee
{
    public class EmployeeContractModel
    {
        [Required(ErrorMessage = "El código del contrato es requerido")]
        public string ContractCode { get; set; }

        [Required(ErrorMessage = "La fecha de inicio del contrato es requerida")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "La fecha de finalización del contrato es requerida")]
        public DateTime EndDate { get; set; }
    }
}
