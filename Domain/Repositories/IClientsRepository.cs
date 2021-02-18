using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Repositories
{
    public interface IClientsRepository : IRepositoryBase<Client, string>
    {
        Task<IEnumerable<Client>> GetClientRequestsAsync();
        Task<Client> GetClientWithUser(string id);

        Task<string> GetClientId(string userId);
    }
}
