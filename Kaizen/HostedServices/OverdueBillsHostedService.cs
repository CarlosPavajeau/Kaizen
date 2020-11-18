using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kaizen.Core.Defines;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Kaizen.HostedServices
{
    public class OverdueBillsHostedService : BackgroundService
    {
        private const int DELAY_TIME = 24 /*Hours*/ * TimeConstants.Minutes * TimeConstants.Seconds * TimeConstants.Milliseconds;

        private readonly IProductInvoicesRepository _productInvoicesRepository;
        private readonly IServiceInvoicesRepository _serviceInvoicesRepository;
        private readonly IUnitWork _unitWork;
        private readonly IHubContext<InvoiceHub> _invoiceHub;

        public OverdueBillsHostedService(
            IProductInvoicesRepository productInvoicesRepository,
            IServiceInvoicesRepository serviceInvoicesRepository,
            IUnitWork unitWork,
            IHubContext<InvoiceHub> invoiceHub
            )
        {
            _productInvoicesRepository = productInvoicesRepository;
            _serviceInvoicesRepository = serviceInvoicesRepository;
            _unitWork = unitWork;
            _invoiceHub = invoiceHub;
        }

        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await FindAndReportExpiredProductInvoices(cancellationToken);

                await FindAndReportExpiredServiceInvoices(cancellationToken);

                await Task.Delay(DELAY_TIME, cancellationToken);
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

            await _invoiceHub.Clients.Group("Administrator").SendAsync("OvedureProductBills", productInvoices, cancellationToken: cancellationToken);
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

            await _invoiceHub.Clients.Group("Administrator").SendAsync("OvedureServiceBills", serviceInvoices, cancellationToken: cancellationToken);
        }
    }
}
