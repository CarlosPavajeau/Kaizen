using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kaizen.Domain.Entities;

namespace Kaizen.Models.ServiceRequest
{
    public class ServiceRequestInputModel : ServiceRequestEditModel
    {
        [Required(ErrorMessage = "El código del cliente es requerido")]
        public string ClientId { get; set; }

        [Required(ErrorMessage = "La periodicidad de las visitas es requerida")]
        public PeriodicityType Periodicity { get; set; }


        public List<string> ServiceCodes { get; set; }
    }
}
