using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Kaizen.HostedServices.ProcessingServices
{
    public class OverdueBills : IScopedProcessingService
    {
        private static readonly int DelayTime = (int)TimeSpan.FromDays(1.0).TotalMilliseconds;

        private readonly IProductInvoicesRepository _productInvoicesRepository;
        private readonly IServiceInvoicesRepository _serviceInvoicesRepository;
        private readonly IUnitWork _unitWork;
        private readonly IHubContext<InvoiceHub> _invoiceHub;

        public OverdueBills(
            IProductInvoicesRepository productInvoicesRepository,
            IServiceInvoicesRepository serviceInvoicesRepository,
            IUnitWork unitWork,
            IHubContext<InvoiceHub> invoiceHub)
        {
            _productInvoicesRepository = productInvoicesRepository;
            _serviceInvoicesRepository = serviceInvoicesRepository;
            _unitWork = unitWork;
            _invoiceHub = invoiceHub;
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await FindAndReportExpiredProductInvoices(cancellationToken);

                await FindAndReportExpiredServiceInvoices(cancellationToken);

                await Task.Delay(DelayTime, cancellationToken);
            }
        }

        private async Task FindAndReportExpiredProductInvoices(CancellationToken cancellationToken)
        {
            IEnumerable<ProductInvoice> productInvoices = await _productInvoicesRepository.GetPendingExpiredProductInvoices();
            foreach (ProductInvoice productInvoice in productInvoices)
            {
                productInvoice.State = InvoiceState.Expired;
                _productInvoicesRepository.Update(productInvoice);
            }

            await _unitWork.SaveAsync();

            await _invoiceHub.Clients.Group("Administrator").SendAsync("OverdueProductBills", productInvoices, cancellationToken);
        }

        private async Task FindAndReportExpiredServiceInvoices(CancellationToken cancellationToken)
        {
            IEnumerable<ServiceInvoice> serviceInvoices = await _serviceInvoicesRepository.GetPendingExpiredServiceInvoices();
            foreach (ServiceInvoice serviceInvoice in serviceInvoices)
            {
                serviceInvoice.State = InvoiceState.Expired;
                _serviceInvoicesRepository.Update(serviceInvoice);
            }

            await _unitWork.SaveAsync();

            await _invoiceHub.Clients.Group("Administrator").SendAsync("OverdueServiceBills", serviceInvoices, cancellationToken);
        }
    }
}
