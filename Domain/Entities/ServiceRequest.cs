using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class ServiceRequest
    {
        [Key]
        public int Code { get; set; }
        public DateTime Date { get; set; }
        public RequestState State { get; set; }


        [ForeignKey("ClientId"), Editable(false)]
        public Client Client { get; set; }

        public List<ServiceRequestService> ServiceRequestsServices { get; set; }

        [NotMapped]
        public List<Service> Services { get; set; }
    }
}
