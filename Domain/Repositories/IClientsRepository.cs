using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kaizen.Domain.Repositories
{
    public interface IClientsRepository : IRepositoryBase<Client, string>
    {
        Task<ClientAddress> GetClientAddressAsync(string clientId);
        void UpdateClientAddress(ClientAddress clientAddress);
        Task<IEnumerable<ContactPerson>> GetClientContactPeopleAsync(string clientId);
    }
}
