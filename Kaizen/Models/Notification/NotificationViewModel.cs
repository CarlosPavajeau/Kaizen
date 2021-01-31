using Kaizen.Domain.Entities;

namespace Kaizen.Models.Notification
{
    public class NotificationViewModel : NotificationEditModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }
        public string Icon { get; set; }
    }
}
