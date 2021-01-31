using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public enum NotificationState
    {
        Pending,
        View
    }

    public class Notification
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }
        public string Icon { get; set; }

        public NotificationState State { get; set; } = NotificationState.Pending;

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public string UserId { get; set; }
    }
}
