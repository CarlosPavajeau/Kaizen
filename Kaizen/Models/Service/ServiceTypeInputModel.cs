using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.Service
{
    public class ServiceTypeInputModel
    {
        [Required(ErrorMessage = "El nombre del tipo de servicio es requerido")]
        public string Name { get; set; }
    }
}
