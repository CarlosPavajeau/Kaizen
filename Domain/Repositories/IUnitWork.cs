using System;
using System.Threading.Tasks;

namespace Kaizen.Domain.Repositories
{
    public interface IUnitWork : IDisposable
    {
        IActivitiesRepository Activities { get; }
        IClientsRepository Clients { get; }
        IApplicationUserRepository ApplicationUsers { get; }
        IEmployeesRepository Employees { get; }
        IEquipmentsRepository Equipments { get; }
        IProductsRepository Products { get; }
        IServicesRepository Services { get; }
        IServiceRequestsRepository ServiceRequests { get; }
        IWorkOrdersRepository WorkOrders { get; }
        Task SaveAsync();
    }
}
