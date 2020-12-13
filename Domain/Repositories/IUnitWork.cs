using System;
using System.Threading.Tasks;

namespace Kaizen.Domain.Repositories
{
    public interface IUnitWork : IDisposable
    {
        Task SaveAsync();
    }
}
