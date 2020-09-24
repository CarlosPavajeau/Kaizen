using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.Base
{
    public class PersonEditModel
    {
        [Required(ErrorMessage = "El primer nombre es requerido")]
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        [Required(ErrorMessage = "El primer apellido es requerido")]
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
    }
}
