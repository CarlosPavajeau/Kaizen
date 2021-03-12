using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Kaizen.Hubs
{
    [Authorize]
    public abstract class BaseHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            if (Context.User?.Identity != null)
            {
                string userRole = Context.User.FindFirst(ClaimTypes.Role)?.Value;
                if (string.IsNullOrEmpty(userRole))
                {
                    return;
                }

                await Groups.AddToGroupAsync(Context.ConnectionId, userRole);
            }

            await base.OnConnectedAsync();
        }
    }
}
