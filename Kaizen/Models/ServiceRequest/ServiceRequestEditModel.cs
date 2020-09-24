using System;
using System.ComponentModel.DataAnnotations;
using Kaizen.Domain.Entities;

namespace Kaizen.Models.ServiceRequest
{
    public class ServiceRequestEditModel
    {
        [Required(ErrorMessage = "La fecha de la solicitud del servicio es requerida")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "El estado de la solicitud del servicio es requerida")]
        public ServiceRequestState State { get; set; }
    }
}
