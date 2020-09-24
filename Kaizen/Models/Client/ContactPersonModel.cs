using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.Client
{
    public class ContactPersonModel
    {
        [Required(ErrorMessage = "El nombre de la persona de contacto es requerido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El teléfono de la persona de contacto es requerido")]
        public string Phonenumber { get; set; }
    }
}
