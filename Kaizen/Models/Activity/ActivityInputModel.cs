using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kaizen.Domain.Entities;

namespace Kaizen.Models.Activity
{
    public class ActivityInputModel : ActivityEditModel
    {
        [Required(ErrorMessage = "La identificación del cliente es requerida")]
        public string ClientId { get; set; }

        [Required(ErrorMessage = "La periodicidad de la actividad es requerida")]
        public PeriodicityType Periodicity { get; set; }

        public List<string> EmployeeCodes { get; set; }
        public List<string> ServiceCodes { get; set; }
    }
}
