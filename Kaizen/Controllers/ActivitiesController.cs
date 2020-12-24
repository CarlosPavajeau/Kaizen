using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using Kaizen.Models.Activity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivitiesRepository _activitiesRepository;
        private readonly IClientsRepository _clientsRepository;
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public ActivitiesController(IActivitiesRepository activitiesRepository, IClientsRepository clientsRepository, IUnitWork unitWork, IMapper mapper)
        {
            _activitiesRepository = activitiesRepository;
            _clientsRepository = clientsRepository;
            _unitWork = unitWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityViewModel>>> GetActivities()
        {
            List<Activity> activities = await _activitiesRepository.GetAll()
                .Include(a => a.Client).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ActivityViewModel>>(activities));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityViewModel>> GetActivity(int id)
        {
            Activity activity = await _activitiesRepository.FindByIdAsync(id);
            if (activity == null)
            {
                return NotFound($"No existe ninguna actividad con el código {id}.");
            }

            return _mapper.Map<ActivityViewModel>(activity);
        }

        [HttpGet("[action]/{employeeId}")]
        public async Task<ActionResult<IEnumerable<ActivityViewModel>>> EmployeeActivities(string employeeId, [FromQuery] DateTime date)
        {
            IEnumerable<Activity> activities = await _activitiesRepository.GetPendingEmployeeActivities(employeeId, date);
            return Ok(_mapper.Map<IEnumerable<ActivityViewModel>>(activities));
        }

        [HttpGet("[action]/{clientId}")]
        public async Task<ActionResult<IEnumerable<ActivityViewModel>>> ClientActivities(string clientId)
        {
            IEnumerable<Activity> activities = await _activitiesRepository.GetPendingClientActivities(clientId);
            return Ok(_mapper.Map<IEnumerable<ActivityViewModel>>(activities));
        }

        [HttpGet("[action]/{clientId}")]
        public async Task<ActionResult<IEnumerable<ActivityViewModel>>> AppliedClientActivities(string clientId)
        {
            IEnumerable<Activity> activities = await _activitiesRepository.GetAppliedClientActivities(clientId);
            return Ok(_mapper.Map<IEnumerable<ActivityViewModel>>(activities));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ActivityViewModel>> PutActivity(int id, ActivityEditModel activityModel)
        {
            Activity activity = await _activitiesRepository.FindByIdAsync(id);
            if (activity is null)
            {
                return BadRequest($"No existe ninguna actividad con el código {id}.");
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
                    return NotFound($"Actualización fallida. No existe ninguna actividad con el código {id}.");
                }
                else
                {
                    throw;
                }
            }

            return _mapper.Map<ActivityViewModel>(activity);
        }

        [HttpPost]
        public async Task<ActionResult<ActivityViewModel>> PostActivity([FromBody] ActivityInputModel activityModel)
        {
            Client client = await _clientsRepository.GetClientWithUser(activityModel.ClientId);
            if (client is null)
            {
                return NotFound($"El cliente con identificación {activityModel.ClientId} no se encuentra registrado.");
            }

            Activity activity = _mapper.Map<Activity>(activityModel);
            activity.Client = client;
            _activitiesRepository.Insert(activity);
            activity.PublishEvent(new SavedActivity(activity));

            await _unitWork.SaveAsync();

            return _mapper.Map<ActivityViewModel>(activity);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ActivityViewModel>> DeleteActivity(int id)
        {
            Activity activity = await _activitiesRepository.FindByIdAsync(id);
            if (activity == null)
            {
                return NotFound($"No existe ninguna actividad con el código {id}.");
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
