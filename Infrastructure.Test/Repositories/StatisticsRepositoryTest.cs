using System;
using System.Threading.Tasks;
using Kaizen.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Infrastructure.Test.Repositories
{
    [TestFixture]
    public class StatisticsRepositoryTest : BaseRepositoryTest
    {
        private IStatisticsRepository _statisticsRepository;

        [SetUp]
        public void SetUp()
        {
            _statisticsRepository = ServiceProvider.GetService<IStatisticsRepository>();
        }

        [Test, Order(0)]
        public void Check_StatisticsRepository()
        {
            Assert.IsNotNull(_statisticsRepository);
        }

        [Test, Order(1)]
        public async Task Register_New_Applied_activity()
        {
            try
            {
                await _statisticsRepository.RegisterNewAppliedActivity();

                Assert.Pass();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.InnerException?.Message);
                Assert.Fail(e.Message);
            }
        }

        [Test, Order(2)]
        public async Task Register_New_Client_Register()
        {
            try
            {
                await _statisticsRepository.RegisterNewClientRegister();

                Assert.Pass();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.InnerException?.Message);
                Assert.Fail(e.Message);
            }
        }

        [Test, Order(3)]
        public async Task Register_New_Client_Visited()
        {
            try
            {
                await _statisticsRepository.RegisterNewClientVisited();

                Assert.Pass();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.InnerException?.Message);
                Assert.Fail(e.Message);
            }
        }

        [Test, Order(4)]
        public async Task Register_Profits()
        {
            try
            {
                await _statisticsRepository.RegisterProfits(5000m);

                Assert.Pass();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.InnerException?.Message);
                Assert.Fail(e.Message);
            }
        }

        [Test, Order(5)]
        public async Task Search_Day_Statistics()
        {
            var dayStatistics = await _statisticsRepository.GetDayStatistics(DateTime.Now);

            Assert.IsNotNull(dayStatistics);
            Assert.AreEqual(1, dayStatistics.ClientsRegistered);
            Assert.AreEqual(1, dayStatistics.ClientsVisited);
            Assert.AreEqual(1, dayStatistics.AppliedActivities);
            Assert.AreEqual(5000m, dayStatistics.Profits);
        }

        [Test, Order(6)]
        public async Task Search_Month_Statistics()
        {
            var monthStatistics = await _statisticsRepository.GetMonthStatistics(DateTime.Now);

            Assert.IsNotNull(monthStatistics);
            Assert.IsNotNull(monthStatistics.DayStatistics);
            Assert.AreEqual(1, monthStatistics.ClientsRegistered);
            Assert.AreEqual(1, monthStatistics.ClientsVisited);
            Assert.AreEqual(1, monthStatistics.AppliedActivities);
            Assert.AreEqual(5000m, monthStatistics.Profits);
        }

        [Test, Order(7)]
        public async Task Search_Year_Statistics()
        {
            var yearStatistics = await _statisticsRepository.GetYearStatistics(DateTime.Now.Year);

            Assert.IsNotNull(yearStatistics);
            Assert.IsNotNull(yearStatistics.MonthStatistics);
            Assert.AreEqual(1, yearStatistics.ClientsRegistered);
            Assert.AreEqual(1, yearStatistics.ClientsVisited);
            Assert.AreEqual(1, yearStatistics.AppliedActivities);
            Assert.AreEqual(5000m, yearStatistics.Profits);
        }
    }
}
