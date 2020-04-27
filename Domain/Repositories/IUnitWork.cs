using System;
using System.Threading.Tasks;

namespace Kaizen.Domain.Repositories
{
    public interface IUnitWork : IDisposable
    {
        IClientsRepository Clients { get; }
        IApplicationUserRepository ApplicationUsers { get; }
        Task Save();
    }
}
