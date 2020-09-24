using System;
using System.ComponentModel.DataAnnotations;
using Kaizen.Domain.Entities;

namespace Kaizen.Models.Activity
{
    public class ActivityEditModel
    {
        [Required(ErrorMessage = "La fecha de la Actividad es requerida")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "El estado de la Actividad es requerido")]
        public ActivityState State { get; set; }
    }
}
