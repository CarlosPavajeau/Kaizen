using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Events;
using Kaizen.Hubs;
using Kaizen.Models.ServiceRequest;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Kaizen.DomainEvents.Handlers
{
    public class OnSavedServiceRequest
    {
        public class Handler : INotificationHandler<DomainEventNotification<SavedServiceRequest>>
        {
            private readonly IHubContext<ServiceRequestHub> _hubContext;
            private readonly IMapper _mapper;

            public Handler(IHubContext<ServiceRequestHub> hubContext, IMapper mapper)
            {
                _hubContext = hubContext;
                _mapper = mapper;
            }

            public async Task Handle(DomainEventNotification<SavedServiceRequest> notification, CancellationToken cancellationToken)
            {
                ServiceRequestViewModel serviceRequestModel = _mapper.Map<ServiceRequestViewModel>(notification.DomainEvent.ServiceRequest);
                await _hubContext.Clients.Groups("Administrator", "OfficeEmployee").SendAsync("NewServiceRequest", serviceRequestModel);
            }
        }
    }
}
