using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class ServiceRequest : RequestBase
    {
        public List<ServiceRequestService> ServiceRequestsServices { get; set; }
        public ServiceRequestState State { get; set; }

        [NotMapped]
        public List<Service> Services { get; set; }
    }
}
