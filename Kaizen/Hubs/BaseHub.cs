using System.Threading.Tasks;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Kaizen.Hubs
{
    [Authorize]
    public abstract class BaseHub : Hub
    {
        private readonly IApplicationUserRepository _applicationUserRepository;

        protected BaseHub(IApplicationUserRepository applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;
        }

        public override async Task OnConnectedAsync()
        {
            if (Context.User?.Identity != null)
            {
                string userName = Context.User.Identity.Name;

                if (string.IsNullOrEmpty(userName))
                {
                    return;
                }

                ApplicationUser user = await _applicationUserRepository.FindByNameAsync(userName);

                if (user is null)
                {
                    return;
                }

                string role = await _applicationUserRepository.GetUserRoleAsync(user);
                await Groups.AddToGroupAsync(Context.ConnectionId, role);
            }

            await base.OnConnectedAsync();
        }
    }
}
