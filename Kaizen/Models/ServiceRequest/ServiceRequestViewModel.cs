using System.Collections.Generic;
using Kaizen.Models.Client;
using Kaizen.Models.Service;

namespace Kaizen.Models.ServiceRequest
{
    public class ServiceRequestViewModel : ServiceRequestInputModel
    {
        public ClientViewModel Client { get; set; }
        public IEnumerable<ServiceViewModel> Services { get; set; }
    }
}
