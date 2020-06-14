using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Events
{
    public class SavedActivity : IDomainEvent
    {
        public SavedActivity(Activity activity)
        {
            Activity = activity;
        }

        public Activity Activity { get; }
    }
}
