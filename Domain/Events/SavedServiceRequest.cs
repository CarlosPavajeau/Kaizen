using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Events
{
    public class SavedServiceRequest : IDomainEvent
    {
        public ServiceRequest ServiceRequest { get; }
        public SavedServiceRequest(ServiceRequest serviceRequest)
        {
            ServiceRequest = serviceRequest;
        }
    }
}
