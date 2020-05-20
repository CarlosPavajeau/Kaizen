using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.Activity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivitiesRepository _activitiesRepository;
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public ActivitiesController(IActivitiesRepository activitiesRepository, IUnitWork unitWork, IMapper mapper)
        {
            _activitiesRepository = activitiesRepository;
            _unitWork = unitWork;
            _mapper = mapper;
        }

        // GET: api/Activities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityViewModel>>> GetActivities()
        {
            List<Activity> activities = await _activitiesRepository.GetAll().ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ActivityViewModel>>(activities));
        }

        // GET: api/Activities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityViewModel>> GetActivity(int id)
        {
            var activity = await _activitiesRepository.FindByIdAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            return _mapper.Map<ActivityViewModel>(activity);
        }

        // PUT: api/Activities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(int id, ActivityEditModel activityModel)
        {
            Activity activity = await _activitiesRepository.FindByIdAsync(id);
            if (activity is null)
            {
                return BadRequest();
            }

            _mapper.Map(activityModel, activity);
            _activitiesRepository.Update(activity);

            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Activities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ActivityViewModel>> PostActivity([FromBody]ActivityInputModel activityModel)
        {
            Activity activity = _mapper.Map<Activity>(activityModel);
            _activitiesRepository.Insert(activity);
            await _unitWork.SaveAsync();

            return _mapper.Map<ActivityViewModel>(activity);
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ActivityViewModel>> DeleteActivity(int id)
        {
            var activity = await _activitiesRepository.FindByIdAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            _activitiesRepository.Delete(activity);
            await _unitWork.SaveAsync();

            return _mapper.Map<ActivityViewModel>(activity);
        }

        private bool ActivityExists(int id)
        {
            return _activitiesRepository.GetAll().Any(a => a.Code == id);
        }
    }
}
