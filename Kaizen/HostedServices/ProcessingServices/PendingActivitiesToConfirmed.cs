using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kaizen.Core.Services;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kaizen.HostedServices.ProcessingServices
{
    public class PendingActivitiesToConfirmed : IScopedProcessingService
    {
        private static readonly int DelayTime = (int)TimeSpan.FromDays(1.0).TotalMilliseconds;

        private readonly IActivitiesRepository _activitiesRepository;
        private readonly IMailService _mailService;
        private readonly IMailTemplate _mailTemplate;
        private readonly IHttpContextAccessor _accessor;
        private readonly LinkGenerator _generator;

        public PendingActivitiesToConfirmed(
            IActivitiesRepository activitiesRepository,
            IMailService mailService,
            IMailTemplate mailTemplate,
            IHttpContextAccessor accessor,
            LinkGenerator generator)
        {
            _activitiesRepository = activitiesRepository;
            _mailService = mailService;
            _mailTemplate = mailTemplate;
            _accessor = accessor;
            _generator = generator;
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                IEnumerable<Activity> pendingActivities = await _activitiesRepository.GetPendingActivitiesToConfirmed();

                foreach (Activity activity in pendingActivities)
                {
                    await SendPendingActivityEmail(activity);
                }

                await Task.Delay(DelayTime, cancellationToken);
            }
        }

        private async Task SendPendingActivityEmail(Activity activity)
        {
            string activityConfirmationLink = GetActivityLink("ConfirmActivity", activity.Code);
            string activityRejectLink = GetActivityLink("RejectActivity", activity.Code);
            string changeDateLink = GetActivityLink("ChangeDate", activity.Code);

            string mailMessage = _mailTemplate.LoadTemplate("PendingActivityToBeConfirmed.html",
                $"{activity.Client.FirstName} {activity.Client.SecondName} {activity.Client.LastName} {activity.Client.SecondLastName}",
                $"{activity.Date}", activityConfirmationLink, activityRejectLink, changeDateLink);

            await _mailService.SendEmailAsync(activity.Client.User.Email, "Actividad pendiente a confirmación", mailMessage,
                true);
        }

        private string GetActivityLink(string action, int activityCode)
        {
            UriBuilder uriBuilder = new UriBuilder(KaizenHttpContext.BaseUrl)
            {
                Path = $"activity_schedule/{action}/{activityCode}",
            };
            return uriBuilder.ToString();
        }
    }
}
