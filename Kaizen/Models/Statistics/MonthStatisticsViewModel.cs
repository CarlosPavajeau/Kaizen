using System;
using System.Collections.Generic;

namespace Kaizen.Models.Statistics
{
    public class MonthStatisticsViewModel : StatisticsViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<DayStatisticsViewModel> DayStatistics { get; set; }
    }
}
