using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;

namespace Kaizen.Infrastructure.Repositories
{
    public class NotificationsRepository : RepositoryBase<Notification, int>, INotificationsRepository
    {
        public NotificationsRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
