using System;
using System.Threading.Tasks;

namespace Kaizen.Domain.Repositories
{
    public interface IUnitWork : IDisposable
    {
        IClientsRepository Clients { get; }
        IApplicationUserRepository ApplicationUsers { get; }
        IEmployeesRepository Employees { get; }
        IEquipmentsRepository Equipments { get; }
        IProductsRepository Products { get; }
        IServicesRepository Services { get; }
        Task SaveAsync();
    }
}
