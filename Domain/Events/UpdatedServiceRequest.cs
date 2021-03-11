using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Events
{
    public class UpdatedServiceRequest : IDomainEvent
    {
        public UpdatedServiceRequest(ServiceRequest serviceRequest)
        {
            ServiceRequest = serviceRequest;
        }

        public ServiceRequest ServiceRequest { get; }
    }
}
