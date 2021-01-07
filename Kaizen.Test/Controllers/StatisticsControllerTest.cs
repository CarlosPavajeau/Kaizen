using System;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.Statistics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Kaizen.Test.Controllers
{
    [TestFixture]
    public class StatisticsControllerTest : BaseControllerTest
    {
        private StatisticsController _statisticsController;
        private Mock<IStatisticsRepository> _statisticsRepository;

        [SetUp]
        public void SetUp()
        {
            _statisticsRepository = new Mock<IStatisticsRepository>();
            _statisticsController =
                new StatisticsController(_statisticsRepository.Object, ServiceProvider.GetService<IMapper>());

            SetUpStatisticsRepository();
        }

        private void SetUpStatisticsRepository()
        {
            _statisticsRepository.Setup(r => r.GetYearStatistics(DateTime.Now.Year)).ReturnsAsync(new YearStatistics
            {
                Id = 1,
                AppliedActivities = 1,
                Year = DateTime.Now.Year
            });

            _statisticsRepository.Setup(r => r.GetMonthStatistics(It.IsAny<DateTime>())).ReturnsAsync(
                new MonthStatistics
                {
                    Id = 1,
                    AppliedActivities = 1
                });

            _statisticsRepository.Setup(r => r.GetDayStatistics(It.IsAny<DateTime>())).ReturnsAsync(new DayStatistics
            {
                Id = 1,
                AppliedActivities = 1
            });
        }

        [Test]
        public async Task Get_Current_Year_Statistics()
        {
            ActionResult<YearStatisticsViewModel> result = await _statisticsController.CurrentYear();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task Get_Current_Month_Statistics()
        {
            ActionResult<MonthStatisticsViewModel> result = await _statisticsController.CurrentMont();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task Get_Current_Day_Statistics()
        {
            var result = await _statisticsController.CurrentDay();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }
    }
}
