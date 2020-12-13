using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Repositories
{
    public class ServiceInvoicesRepository : RepositoryBase<ServiceInvoice, int>, IServiceInvoicesRepository
    {
        public ServiceInvoicesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public override async Task<ServiceInvoice> FindByIdAsync(int id)
        {
            return await GetAll().Include(s => s.ServiceInvoiceDetails).Include(s => s.Client)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<ServiceInvoice>> GetClientInvoices(string clientId)
        {
            return await GetAll().Include(s => s.ServiceInvoiceDetails).Include(s => s.Client)
                .Where(s => s.ClientId == clientId).ToListAsync();
        }

        public async Task<IEnumerable<ServiceInvoice>> GetPendingExpiredServiceInvoices()
        {
            return await GetAll().Include(s => s.Client).Include(s => s.ServiceInvoiceDetails)
                .Where(s => s.State == InvoiceState.Generated && (DateTime.Now - s.GenerationDate).Days >= Invoice.DayLimits)
                .ToListAsync();
        }
    }
}
