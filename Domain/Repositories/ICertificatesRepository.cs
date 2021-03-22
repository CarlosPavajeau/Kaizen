using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Repositories
{
    public interface ICertificatesRepository : IRepositoryBase<Certificate, int>
    {
        Task<IEnumerable<Certificate>> GetClientCertificates(string clientId);
    }
}
