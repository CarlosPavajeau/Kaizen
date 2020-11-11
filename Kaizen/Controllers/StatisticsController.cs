using System;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly IMapper _mapper;

        public StatisticsController(IStatisticsRepository statisticsRepository, IMapper mapper)
        {
            _statisticsRepository = statisticsRepository;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<YearStatisticsViewModel>> CurrentYear()
        {
            YearStatistics yearStatistics = await _statisticsRepository.GetYearStatistics(DateTime.Now.Year);
            return _mapper.Map<YearStatisticsViewModel>(yearStatistics);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<MonthStatisticsViewModel>> CurrentMont()
        {
            MonthStatistics monthStatistics = await _statisticsRepository.GetMonthStatistics(DateTime.Now);
            return _mapper.Map<MonthStatisticsViewModel>(monthStatistics);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<DayStatisticsViewModel>> CurrentDay()
        {
            DayStatistics dayStatistics = await _statisticsRepository.GetDayStatistics(DateTime.Now);
            return _mapper.Map<DayStatisticsViewModel>(dayStatistics);
        }
    }
}
