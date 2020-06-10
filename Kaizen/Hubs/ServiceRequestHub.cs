using Kaizen.Domain.Repositories;

namespace Kaizen.Hubs
{
    public class ServiceRequestHub : BaseHub
    {
        public ServiceRequestHub(IApplicationUserRepository applicationUserRepository) : base(applicationUserRepository)
        {
        }
    }
}
