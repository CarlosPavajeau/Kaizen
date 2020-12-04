using System.ComponentModel.DataAnnotations;
using Kaizen.Domain.Entities;

namespace Kaizen.Models.Product
{
    public class ProductEditModel
    {
        [Required(ErrorMessage = "El nombre del producto es requerido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descripción del producto es requerida")]
        public string Description { get; set; }

        [Required(ErrorMessage = "La cantida del producto es requerida")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Los meses de aplicación del producto son requeridos")]
        public ApplicationMonths ApplicationMonths { get; set; }

        [Required(ErrorMessage = "La presentación del producto es requerida")]
        public string Presentation { get; set; }

        [Required(ErrorMessage = "El precio del producto es requerido")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "El registro sanitario del producto es requerido")]
        public string HealthRegister { get; set; }

        [Required(ErrorMessage = "La ficha técnica del producto es requerida")]
        public string DataSheet { get; set; }

        [Required(ErrorMessage = "La tarjeta de seguridad del producto es requerida")]
        public string SafetySheet { get; set; }

        [Required(ErrorMessage = "La tarjeta de emergencia del producto es requerida")]
        public string EmergencyCard { get; set; }
    }
}
