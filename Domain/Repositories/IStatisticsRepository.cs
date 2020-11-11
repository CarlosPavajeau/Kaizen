using System;
using System.Threading.Tasks;
using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Repositories
{
    public interface IStatisticsRepository : IRepositoryBase<Statistics, int>
    {
        Task RegisterNewAppliedActivity();
        Task RegisterNewClientVisited();
        Task RegisterNewClientRegister();
        Task RegisterProfits(decimal profits);

        Task<DayStatistics> GetDayStatistics(DateTime date);
        Task<MonthStatistics> GetMonthStatistics(DateTime date);
        Task<YearStatistics> GetYearStatistics(int year);
    }
}
