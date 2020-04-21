using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class ServiceRequest
    {
        [Key, MaxLength(15)]
        public string Code { get; set; }
        public DateTime Date { get; set; }

        
        [ForeignKey("ClientId"), Editable(false)]
        public Client Client { get; set; }

        [ForeignKey("ServiceCode")]
        public Service Service { get; set; }
    }
}
