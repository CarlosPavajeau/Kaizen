using System;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Repositories
{
    public class StatisticsRepository : RepositoryBase<Statistics, int>, IStatisticsRepository
    {
        public StatisticsRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<DayStatistics> GetDayStatistics(DateTime date)
        {
            return await ApplicationDbContext.DayStatistics
                .FirstOrDefaultAsync(d => d.Date.Year == date.Year && d.Date.Month == date.Month && d.Date.Day == date.Day);
        }

        public async Task<MonthStatistics> GetMonthStatistics(DateTime date)
        {
            return await ApplicationDbContext.MonthStatistics
                .Include(m => m.DayStatistics)
                .FirstOrDefaultAsync(m => m.Date.Year == date.Year && m.Date.Month == date.Month);
        }

        public async Task<YearStatistics> GetYearStatistics(int year)
        {
            return await ApplicationDbContext.YearStatistics
                .Include(y => y.MonthStatistics)
                .ThenInclude(m => m.DayStatistics)
                .FirstOrDefaultAsync(y => y.Year == year);
        }

        public async Task RegisterNewAppliedActivity()
        {
            DateTime currentDate = DateTime.Now;

            YearStatistics yearStatistics = await FindOrCreateStatistics();
            MonthStatistics monthStatistics = yearStatistics.MonthStatistics.FirstOrDefault(m => m.Date.Month == currentDate.Month);
            DayStatistics dayStatistics = monthStatistics.DayStatistics.FirstOrDefault(d => d.Date.Day == currentDate.Day);

            ++yearStatistics.AppliedActivities;
            ++monthStatistics.AppliedActivities;
            ++dayStatistics.AppliedActivities;

            await UpdateStatistics(yearStatistics, monthStatistics, dayStatistics);
        }

        public async Task RegisterNewClientRegister()
        {
            DateTime currentDate = DateTime.Now;

            YearStatistics yearStatistics = await FindOrCreateStatistics();
            MonthStatistics monthStatistics = yearStatistics.MonthStatistics.FirstOrDefault(m => m.Date.Month == currentDate.Month);
            DayStatistics dayStatistics = monthStatistics.DayStatistics.FirstOrDefault(d => d.Date.Day == currentDate.Day);

            ++yearStatistics.ClientsRegistered;
            ++monthStatistics.ClientsRegistered;
            ++dayStatistics.ClientsRegistered;

            await UpdateStatistics(yearStatistics, monthStatistics, dayStatistics);
        }

        public async Task RegisterNewClientVisited()
        {
            DateTime currentDate = DateTime.Now;

            YearStatistics yearStatistics = await FindOrCreateStatistics();
            MonthStatistics monthStatistics = yearStatistics.MonthStatistics.FirstOrDefault(m => m.Date.Month == currentDate.Month);
            DayStatistics dayStatistics = monthStatistics.DayStatistics.FirstOrDefault(d => d.Date.Day == currentDate.Day);

            ++yearStatistics.ClientsVisited;
            ++monthStatistics.ClientsVisited;
            ++dayStatistics.ClientsVisited;

            await UpdateStatistics(yearStatistics, monthStatistics, dayStatistics);
        }

        public async Task RegisterProfits(decimal profits)
        {
            DateTime currentDate = DateTime.Now;

            YearStatistics yearStatistics = await FindOrCreateStatistics();
            MonthStatistics monthStatistics = yearStatistics.MonthStatistics.FirstOrDefault(m => m.Date.Month == currentDate.Month);
            DayStatistics dayStatistics = monthStatistics.DayStatistics.FirstOrDefault(d => d.Date.Day == currentDate.Day);

            yearStatistics.Profits += profits;
            monthStatistics.Profits += profits;
            dayStatistics.Profits += profits;

            await UpdateStatistics(yearStatistics, monthStatistics, dayStatistics);
        }

        private async Task UpdateStatistics(YearStatistics yearStatistics, MonthStatistics monthStatistics, DayStatistics dayStatistics)
        {
            ApplicationDbContext.YearStatistics.Update(yearStatistics);
            ApplicationDbContext.MonthStatistics.Update(monthStatistics);
            ApplicationDbContext.DayStatistics.Update(dayStatistics);
            await ApplicationDbContext.SaveChangesAsync();
        }

        private async Task<YearStatistics> FindOrCreateStatistics()
        {
            DateTime currentDate = DateTime.Now;
            YearStatistics yearStatistics = await GetYearStatistics(currentDate.Year);
            if (yearStatistics is null)
            {
                yearStatistics = new YearStatistics { Year = currentDate.Year, Date = currentDate };
                ApplicationDbContext.YearStatistics.Add(yearStatistics);

                await ApplicationDbContext.SaveChangesAsync();
            }

            MonthStatistics monthStatistics = yearStatistics.MonthStatistics.FirstOrDefault(m => m.Date.Month == currentDate.Month);
            if (monthStatistics is null)
            {
                monthStatistics = new MonthStatistics { Date = currentDate };
                yearStatistics.MonthStatistics.Add(monthStatistics);
                ApplicationDbContext.YearStatistics.Update(yearStatistics);

                await ApplicationDbContext.SaveChangesAsync();
            }

            DayStatistics dayStatistics = monthStatistics.DayStatistics.FirstOrDefault(d => d.Date.Day == currentDate.Day);
            if (dayStatistics is null)
            {
                dayStatistics = new DayStatistics { Date = currentDate };
                monthStatistics.DayStatistics.Add(dayStatistics);
                ApplicationDbContext.MonthStatistics.Update(monthStatistics);

                await ApplicationDbContext.SaveChangesAsync();
            }

            return yearStatistics;
        }
    }
}
