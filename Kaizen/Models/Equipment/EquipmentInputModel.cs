using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.Equipment
{
    public class EquipmentInputModel : EquipmentEditModel
    {
        [Required(ErrorMessage = "El código del equipo es requerido")]
        public string Code { get; set; }
    }
}
