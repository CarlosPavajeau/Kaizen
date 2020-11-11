using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Domain.Entities
{
    public class YearStatistics : Statistics
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Year { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public List<MonthStatistics> MonthStatistics { get; set; } = new List<MonthStatistics>();
    }
}
