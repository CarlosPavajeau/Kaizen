using System;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.WorkOrder
{
    public class WorkOrderInputModel : WorkOrderEditModel
    {
        [Required(ErrorMessage = "El código del sector es requerido")]
        public int SectorId { get; set; }

        [Required(ErrorMessage = "La fecha de ejecución es requerida")]
        public DateTime ExecutionDate { get; set; }

        [Required(ErrorMessage = "La fecha de llegada es requerida")]
        public DateTime ArrivalTime { get; set; }

        [Required(ErrorMessage = "El código de la actividad es requerido")]
        public int ActivityCode { get; set; }

        [Required(ErrorMessage = "El código del empleado es requerido")]
        public string EmployeeId { get; set; }
    }
}
