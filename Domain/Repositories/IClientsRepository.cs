using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Repositories
{
    public interface IClientsRepository : IRepositoryBase<Client, string>
    {
        void UpdateClientAddress(ClientAddress clientAddress);

        Task<IEnumerable<Client>> GetClientRequestsAsync();
    }
}
