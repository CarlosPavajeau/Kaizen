using Kaizen.Domain.Repositories;

namespace Kaizen.Hubs
{
    public class ActivityHub : BaseHub
    {
        public ActivityHub(IApplicationUserRepository applicationUserRepository) : base(applicationUserRepository)
        {
        }
    }
}
