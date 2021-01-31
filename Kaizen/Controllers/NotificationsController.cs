using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationsRepository _notificationsRepository;
        private readonly IMapper _mapper;
        private readonly IUnitWork _unitWork;

        public NotificationsController(INotificationsRepository notificationsRepository, IUnitWork unitWork,
            IMapper mapper)
        {
            _notificationsRepository = notificationsRepository;
            _unitWork = unitWork;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<NotificationViewModel>>> GetNotifications(string userId)
        {
            var notifications = await _notificationsRepository
                .Where(n => n.UserId == userId && n.State == NotificationState.Pending)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<NotificationViewModel>>(notifications));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<NotificationViewModel>> PutNotification(int id,
            [FromBody] NotificationEditModel notificationEditModel)
        {
            var notification = await _notificationsRepository.FindByIdAsync(id);
            if (notification is null)
            {
                return BadRequest($"No existe una notificación con el código {id}.");
            }

            _mapper.Map(notificationEditModel, notification);
            _notificationsRepository.Update(notification);

            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateException)
            {
                if (!NotificationExists(id))
                {
                    return NotFound($"Actualizacón fallida. No existe ninguna notificación con el código {id}.");
                }

                throw;
            }

            return _mapper.Map<NotificationViewModel>(notification);
        }

        private bool NotificationExists(int id)
        {
            return _notificationsRepository.GetAll().Any(n => n.Id == id);
        }
    }
}
