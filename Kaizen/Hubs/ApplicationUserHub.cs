using Kaizen.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Kaizen.Hubs
{
    [Authorize]
    public class ApplicationUserHub : BaseHub
    {
    }
}
