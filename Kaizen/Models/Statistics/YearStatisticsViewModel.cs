using System.Collections.Generic;

namespace Kaizen.Models.Statistics
{
    public class YearStatisticsViewModel : StatisticsViewModel
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public List<MonthStatisticsViewModel> MonthStatistics { get; set; }
    }
}
