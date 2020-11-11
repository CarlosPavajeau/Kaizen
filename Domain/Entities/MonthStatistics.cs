using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Domain.Entities
{
    public class MonthStatistics : Statistics
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public List<DayStatistics> DayStatistics { get; set; } = new List<DayStatistics>();
    }
}
