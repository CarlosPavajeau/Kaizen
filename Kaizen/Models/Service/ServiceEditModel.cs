using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kaizen.Validations;

namespace Kaizen.Models.Service
{
    public class ServiceEditModel
    {
        [Required(ErrorMessage = "El nombre del servicio es requerido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El costo del servicio es requerido")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "El tipo de servicio es requerido")]
        public int ServiceTypeId { get; set; }

        [NotNullOrEmptyCollection(ErrorMessage = "Se deben asignar equipos al servicio")]
        public List<string> EquipmentCodes { get; set; }

        [NotNullOrEmptyCollection(ErrorMessage = "Se deben asignar productos al servicio")]
        public List<string> ProductCodes { get; set; }
        [NotNullOrEmptyCollection(ErrorMessage = "Se deben asignar empleados al servicio")]
        public List<string> EmployeeCodes { get; set; }
    }
}
