using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.Product
{
    public class ProductInputModel : ProductEditModel
    {
        [Required(ErrorMessage = "El código del producto es requerido")]
        public string Code { get; set; }
    }
}
