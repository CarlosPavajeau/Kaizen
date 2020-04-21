using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Domain.Entities
{
    public class ActivitySchedule
    {
        [Key]
        public int Year { get; set; }

        public List<Activity> Activities { get; set; }
    }
}
