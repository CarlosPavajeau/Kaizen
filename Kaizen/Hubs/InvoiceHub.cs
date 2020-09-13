using Kaizen.Domain.Repositories;

namespace Kaizen.Hubs
{
    public class InvoiceHub : BaseHub
    {
        public InvoiceHub(IApplicationUserRepository applicationUserRepository) : base(applicationUserRepository)
        {
        }
    }
}
