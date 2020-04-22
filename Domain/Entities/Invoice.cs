using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public enum InvoiceState
    {
        Generated,
        Regenerated,
        Paid,
        Expired
    }
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public InvoiceState State { get; set; }
        public decimal IVA { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }
    }
}
