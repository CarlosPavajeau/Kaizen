using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kaizen.Core.Defines;
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
        private static readonly int DELAY_TIME = 24 /*Hours*/ * TimeConstants.Minutes * TimeConstants.Seconds * TimeConstants.Milliseconds;

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

        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
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
                    string activityConfirmationLink = _generator.GetUriByAction(_accessor.HttpContext, $"ConfirmActivity/{activity.Code}", "activity_schedule");
                    string activityRejectLink = _generator.GetUriByAction(_accessor.HttpContext, $"RejectActivity/{activity.Code}", "activity_schedule");
                    string changeDateLink = _generator.GetUriByAction(_accessor.HttpContext, $"ChangeDate/{activity.Code}", "activity_schedule");

                    string mailMessage = _mailTemplate.LoadTemplate("PendingActivityToBeConfirmed.html",
                        $"{activity.Client.FirstName} {activity.Client.SecondName} {activity.Client.LastName} {activity.Client.SecondLastName}",
                        $"{activity.Date}", activityConfirmationLink, activityRejectLink, changeDateLink);

                    await _mailService.SendEmailAsync(activity.Client.User.Email, "Actividad pendiente a confirmación", mailMessage, true);
                }

                await Task.Delay(DELAY_TIME, cancellationToken);
            }
        }
    }
}
