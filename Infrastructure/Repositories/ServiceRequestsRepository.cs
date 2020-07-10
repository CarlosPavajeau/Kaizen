using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Repositories
{
    public class ServiceRequestsRepository : RepositoryBase<ServiceRequest, int>, IServiceRequestsRepository
    {
        public ServiceRequestsRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public override async Task<ServiceRequest> FindByIdAsync(int id)
        {
            ServiceRequest serviceRequest = await ApplicationDbContext.ServiceRequests.Include(s => s.Client)
                .Include(s => s.ServiceRequestsServices).ThenInclude(s => s.Service)
                .Where(s => s.Code == id).FirstOrDefaultAsync();

            MapServices(serviceRequest);

            return serviceRequest;
        }

        private void MapServices(ServiceRequest serviceRequest)
        {
            if (serviceRequest != null)
            {
                serviceRequest.Services = new List<Service>();
                foreach (ServiceRequestService serviceRequestService in serviceRequest.ServiceRequestsServices)
                {
                    serviceRequest.Services.Add(serviceRequestService.Service);
                }
            }
        }

        public async Task<ServiceRequest> GetPendingCustomerServiceRequest(string clientId)
        {
            ServiceRequest serviceRequest = await ApplicationDbContext.ServiceRequests.Include(s => s.Client)
                .Include(s => s.ServiceRequestsServices)
                .ThenInclude(s => s.Service)
                .Where(s => s.ClientId == clientId && (s.State == ServiceRequestState.Pending || s.State == ServiceRequestState.PendingSuggestedDate))
                .FirstOrDefaultAsync();

            MapServices(serviceRequest);

            return serviceRequest;
        }
    }
}
