using System.Collections.Generic;
using Kaizen.Domain.Entities;

namespace Kaizen.Models.ServiceRequest
{
    public class ServiceRequestInputModel : ServiceRequestEditModel
    {
        public string ClientId { get; set; }
        public PeriodicityType Periodicity { get; set; }
        public List<string> ServiceCodes { get; set; }
    }
}
