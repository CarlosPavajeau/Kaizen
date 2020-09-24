using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.Service
{
    public class ServiceInputModel : ServiceEditModel
    {
        [Required(ErrorMessage = "El código del servicio es requerido")]
        public string Code { get; set; }
    }
}
