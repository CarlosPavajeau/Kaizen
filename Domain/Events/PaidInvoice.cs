using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Events
{
    public class PaidInvoice : IDomainEvent
    {
        public PaidInvoice(Invoice invoice)
        {
            Invoice = invoice;
        }

        public Invoice Invoice { get; }
    }
}
