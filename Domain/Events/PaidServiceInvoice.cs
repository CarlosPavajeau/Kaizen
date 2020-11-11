using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Events
{
    public class PaidServiceInvoice : IDomainEvent
    {
        public PaidServiceInvoice(ServiceInvoice serviceInvoice)
        {
            ServiceInvoice = serviceInvoice;
        }

        public ServiceInvoice ServiceInvoice { get; }
    }
}
