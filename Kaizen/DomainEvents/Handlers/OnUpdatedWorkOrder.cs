using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using MediatR;

namespace Kaizen.DomainEvents.Handlers
{
    public class OnUpdatedWorkOrder
    {
        public class Handler : INotificationHandler<DomainEventNotification<UpdatedWorkOrder>>
        {
            private readonly IUnitWork _unitWork;
            private readonly IServiceInvoicesRepository _serviceInvoicesRepository;
            private readonly IStatisticsRepository _statisticsRepository;
            public Handler(IUnitWork unitWork, IServiceInvoicesRepository serviceInvoicesRepository, IStatisticsRepository statisticsRepository)
            {
                _unitWork = unitWork;
                _serviceInvoicesRepository = serviceInvoicesRepository;
                _statisticsRepository = statisticsRepository;
            }

            public async Task Handle(DomainEventNotification<UpdatedWorkOrder> notification, CancellationToken cancellationToken)
            {
                WorkOrder workOrder = notification.DomainEvent.WorkOrder;

                if (workOrder.WorkOrderState == WorkOrderState.Valid)
                {
                    Activity appliedActivity = await UpdateActivityToApplied(workOrder.ActivityCode);
                    GenerateInvoice(appliedActivity);

                    await _statisticsRepository.RegisterNewAppliedActivity();
                    await _unitWork.SaveAsync();
                }
            }

            private async Task<Activity> UpdateActivityToApplied(int activityCode)
            {
                Activity activity = await _unitWork.Activities.FindByIdAsync(activityCode);
                if (activity != null)
                {
                    activity.State = ActivityState.Applied;
                    _unitWork.Activities.Update(activity);
                }

                return activity;
            }

            private void GenerateInvoice(Activity activity)
            {
                if (activity is null)
                {
                    return;
                }

                List<Service> services = activity.ActivitiesServices.Select(s => s.Service).ToList();

                ServiceInvoice serviceInvoice = new ServiceInvoice()
                {
                    Client = activity.Client,
                    ClientId = activity.ClientId,
                    PaymentMethod = PaymentMethod.None,
                    State = InvoiceState.Generated,
                    IVA = 0.19M,
                    GenerationDate = DateTime.Now
                };

                services.ForEach(service =>
                {
                    serviceInvoice.AddDetail(service);
                });

                serviceInvoice.CalculateTotal();

                _serviceInvoicesRepository.Insert(serviceInvoice);
            }
        }
    }
}
