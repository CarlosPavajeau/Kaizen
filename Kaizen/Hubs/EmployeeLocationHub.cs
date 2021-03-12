using System.Threading.Tasks;
using Kaizen.Domain.Repositories;
using Kaizen.Models.Employee;
using Microsoft.AspNetCore.SignalR;

namespace Kaizen.Hubs
{
    public class EmployeeLocationHub : BaseHub
    {
        public async Task UpdateEmployeeLocation(EmployeeLocation employeeLocation)
        {
            await Clients.Group("Administrator").SendAsync("OnUpdateEmployeeLocation", employeeLocation);
        }
    }
}
