using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Models.Statistics;

namespace Kaizen.Mappers
{
    public class StatisticsMapperProfile : Profile
    {
        public StatisticsMapperProfile()
        {
            CreateMap<DayStatistics, DayStatisticsViewModel>();
            CreateMap<MonthStatistics, MonthStatisticsViewModel>();
            CreateMap<YearStatistics, YearStatisticsViewModel>();
        }
    }
}
