using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class RequestBase : Entity
    {
        [Key]
        public int Code { get; set; }
        public DateTime Date { get; set; }
        public RequestState State { get; set; }

        [Required, MaxLength(10)]
        public string ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        public PeriodicityType Periodicity { get; set; }
    }
}
