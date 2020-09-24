using System.ComponentModel.DataAnnotations;
using Kaizen.Domain.Entities;

namespace Kaizen.Models.Base
{
    public class InvoiceEditModel
    {
        [Required(ErrorMessage = "El estado de la factura es requerido")]
        public InvoiceState State { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
