using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kaizen.Core.Services;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.HostedServices
{
    public class PendingActivitiesToBeConfirmedHostedService : BackgroundService
    {
        private static readonly int DelayTime = TimeSpan.FromDays(1.0).Milliseconds;

        private readonly IActivitiesRepository _activitiesRepository;
        private readonly IMailService _mailService;
        private readonly IMailTemplate _mailTemplate;
        private readonly IHttpContextAccessor _accessor;
        private readonly LinkGenerator _generator;

        public PendingActivitiesToBeConfirmedHostedService(
            IActivitiesRepository activitiesRepository,
            IMailService mailService,
            IMailTemplate mailTemplate,
            IHttpContextAccessor accessor,
            LinkGenerator generator
            )
        {
            _activitiesRepository = activitiesRepository;
            _mailService = mailService;
            _mailTemplate = mailTemplate;
            _accessor = accessor;
            _generator = generator;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                DateTime today = DateTime.Now;

                List<Activity> pendingActivities = await _activitiesRepository
                    .Where(a => a.State == ActivityState.Pending && (a.Date - today).Days <= 3 && (a.Date - today).Days > 0)
                    .Include(a => a.Client)
                    .ThenInclude(c => c.User)
                    .ToListAsync(cancellationToken: cancellationToken);

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
            return _generator.GetUriByAction(_accessor.HttpContext, $"{action}/{activityCode}", "activity_schedule");
        }
    }
}
