using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using Kaizen.Models.ServiceRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceRequestsController : ControllerBase
    {
        private readonly IServiceRequestsRepository _serviceRequestsRepository;
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public ServiceRequestsController(IServiceRequestsRepository serviceRequestsRepository, IUnitWork unitWork, IMapper mapper)
        {
            _serviceRequestsRepository = serviceRequestsRepository;
            _unitWork = unitWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRequestViewModel>>> GetServiceRequests()
        {
            List<ServiceRequest> serviceRequests = await _serviceRequestsRepository.GetAll()
                .Where(s => s.State == ServiceRequestState.Pending).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ServiceRequestViewModel>>(serviceRequests));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRequestViewModel>> GetServiceRequest(int id)
        {
            ServiceRequest serviceRequest = await _serviceRequestsRepository.FindByIdAsync(id);
            if (serviceRequest == null)
            {
                return NotFound($"No existe ninguna solicitud de servicio con el código {id}.");
            }

            return _mapper.Map<ServiceRequestViewModel>(serviceRequest);
        }

        [HttpGet("[action]/{clientId}")]
        public async Task<ActionResult<ServiceRequestViewModel>> PendingServiceRequest(string clientId)
        {
            ServiceRequest serviceRequest = await _serviceRequestsRepository.GetPendingCustomerServiceRequest(clientId);
            if (serviceRequest is null)
            {
                return NotFound($"No existe ninguna solicitud de servicio pendiente para el cliente identificaco con {clientId}.");
            }

            return _mapper.Map<ServiceRequestViewModel>(serviceRequest);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceRequestViewModel>> PutServiceRequest(int id, ServiceRequestEditModel serviceRequestModel)
        {
            ServiceRequest serviceRequest = await _serviceRequestsRepository.FindByIdAsync(id);
            if (serviceRequest is null)
            {
                return BadRequest($"No existe ninguna solicitud de servicio con el código {id}.");
            }

            _mapper.Map(serviceRequestModel, serviceRequest);
            serviceRequest.PublishEvent(new UpdatedServiceRequest(serviceRequest));
            _serviceRequestsRepository.Update(serviceRequest);

            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceRequestExists(id))
                {
                    return NotFound($"Actualización fallida. No existe ninguna solicitud de servicio con el código {id}.");
                }

                throw;
            }

            return _mapper.Map<ServiceRequestViewModel>(serviceRequest);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceRequestViewModel>> PostServiceRequest([FromBody] ServiceRequestInputModel serviceRequestModel)
        {
            ServiceRequest serviceRequest = _mapper.Map<ServiceRequest>(serviceRequestModel);
            _serviceRequestsRepository.Insert(serviceRequest);
            serviceRequest.PublishEvent(new SavedServiceRequest(serviceRequest));

            await _unitWork.SaveAsync();

            return _mapper.Map<ServiceRequestViewModel>(serviceRequest);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceRequestViewModel>> DeleteServiceRequest(int id)
        {
            ServiceRequest serviceRequest = await _serviceRequestsRepository.FindByIdAsync(id);
            if (serviceRequest == null)
            {
                return NotFound($"No existe ninguna solicitud de servicio con el código {id}.");
            }

            _serviceRequestsRepository.Delete(serviceRequest);
            await _unitWork.SaveAsync();

            return _mapper.Map<ServiceRequestViewModel>(serviceRequest);
        }

        private bool ServiceRequestExists(int id)
        {
            return _serviceRequestsRepository.GetAll().Any(s => s.Code == id);
        }
    }
}
