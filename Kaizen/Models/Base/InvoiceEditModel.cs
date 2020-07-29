using Kaizen.Domain.Entities;

namespace Kaizen.Models.Base
{
    public class InvoiceEditModel
    {
        public InvoiceState State { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
