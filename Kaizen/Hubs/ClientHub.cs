using Kaizen.Domain.Repositories;

namespace Kaizen.Hubs
{
    public class ClientHub : BaseHub
    {
        public ClientHub(IApplicationUserRepository applicationUserRepository) : base(applicationUserRepository)
        {
        }
    }
}
