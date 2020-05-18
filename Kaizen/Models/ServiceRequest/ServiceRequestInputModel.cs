using System.Collections.Generic;

namespace Kaizen.Models.ServiceRequest
{
    public class ServiceRequestInputModel : ServiceRequestEditModel
    {
        public string ClientId { get; set; }
        public List<string> ServiceCodes { get; set; }
    }
}
