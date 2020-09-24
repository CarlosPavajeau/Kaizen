using System;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.Equipment
{
    public class EquipmentEditModel
    {
        [Required(ErrorMessage = "El nombre del equipo es requerido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La fecha de mantenimiento del equipo es requerida")]
        public DateTime MaintenanceDate { get; set; }

        [Required(ErrorMessage = "La descripción del equipo es requerida")]
        public string Description { get; set; }

        [Required(ErrorMessage = "La cantidad de equipos es requerida")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "El precio del equipo es requerido")]
        public decimal Price { get; set; }
    }
}
