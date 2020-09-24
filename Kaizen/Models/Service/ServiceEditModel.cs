using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public List<string> EquipmentCodes { get; set; }
        public List<string> ProductCodes { get; set; }
        public List<string> EmployeeCodes { get; set; }
    }
}
