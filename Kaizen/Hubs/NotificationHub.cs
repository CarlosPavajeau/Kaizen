using Kaizen.Domain.Repositories;

namespace Kaizen.Hubs
{
    public class NotificationHub : BaseHub
    {
        public NotificationHub(IApplicationUserRepository applicationUserRepository) : base(applicationUserRepository)
        {
        }
    }
}
