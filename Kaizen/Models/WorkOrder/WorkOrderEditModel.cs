using System;
using System.ComponentModel.DataAnnotations;
using Kaizen.Domain.Entities;

namespace Kaizen.Models.WorkOrder
{
    public class WorkOrderEditModel
    {
        [Required(ErrorMessage = "El estado de la orden de trabajo es requerido")]
        public WorkOrderState WorkOrderState { get; set; }

        [Required(ErrorMessage = "La fecha de salida es requerida")]
        public DateTime DepatureTime { get; set; }

        [Required(ErrorMessage = "La fecha de válidez es requerida")]
        public DateTime Validity { get; set; }

        [Required(ErrorMessage = "La firma del cliente es requerida")]
        public string ClientSignature { get; set; }

        [Required(ErrorMessage = "Las observaciones son requeridas")]
        public string Observations { get; set; }
    }
}
