using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Events
{
    public class SavedServiceRequest : IDomainEvent
    {
        public SavedServiceRequest(ServiceRequest serviceRequest)
        {
            ServiceRequest = serviceRequest;
        }

        public ServiceRequest ServiceRequest { get; }
    }
}
