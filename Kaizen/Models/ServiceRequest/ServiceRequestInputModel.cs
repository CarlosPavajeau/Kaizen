using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kaizen.Domain.Entities;
using Kaizen.Validations;

namespace Kaizen.Models.ServiceRequest
{
    public class ServiceRequestInputModel : ServiceRequestEditModel
    {
        [Required(ErrorMessage = "El código del cliente es requerido")]
        public string ClientId { get; set; }

        [Required(ErrorMessage = "La periodicidad de las visitas es requerida")]
        public PeriodicityType Periodicity { get; set; }

        [NotNullOrEmptyCollection(ErrorMessage = "Se deben asignar servicios a la solicitud")]
        public List<string> ServiceCodes { get; set; }
    }
}
