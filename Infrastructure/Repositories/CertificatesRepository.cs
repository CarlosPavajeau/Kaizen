using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Repositories
{
    public class CertificatesRepository : RepositoryBase<Certificate, int>, ICertificatesRepository
    {
        public CertificatesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public override async Task<Certificate> FindByIdAsync(int id)
        {
            return await ApplicationDbContext.Certificates
                .Include(c => c.WorkOrder)
                .ThenInclude(w => w.Activity)
                .ThenInclude(a => a.Client)
                .Include(c => c.WorkOrder)
                .ThenInclude(w => w.Activity)
                .ThenInclude(a => a.ActivitiesServices)
                .ThenInclude(a => a.Service)
                .Include(c => c.WorkOrder)
                .ThenInclude(w => w.Employee)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Certificate>> GetClientCertificates(string clientId)
        {
            return await ApplicationDbContext.Certificates
                .Where(c => c.WorkOrder.Activity.ClientId == clientId)
                .ToListAsync();
        }
    }
}
