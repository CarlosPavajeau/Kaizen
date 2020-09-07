using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public abstract class Invoice : Entity
    {
        public const int DayLimits = 30;

        [Key]
        public int Id { get; set; }
        public InvoiceState State { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal IVA { get; set; } = 0.19M;
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        public string ClientId { get; set; }

        [DataType(DataType.Date)]
        public DateTime GenerationDate { get; set; }

        public void CalculateTotal()
        {
            Total = SubTotal * (1 + IVA);
        }
    }
}
