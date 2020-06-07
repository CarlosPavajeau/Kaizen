using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.ServiceRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        // GET: api/ServiceRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRequestViewModel>>> GetServiceRequests()
        {
            List<ServiceRequest> serviceRequests = await _serviceRequestsRepository.GetAll().ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ServiceRequestViewModel>>(serviceRequests));
        }

        // GET: api/ServiceRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRequestViewModel>> GetServiceRequest(int id)
        {
            ServiceRequest serviceRequest = await _serviceRequestsRepository.FindByIdAsync(id);

            if (serviceRequest == null)
            {
                return NotFound();
            }

            return _mapper.Map<ServiceRequestViewModel>(serviceRequest);
        }

        // PUT: api/ServiceRequests/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceRequest(int id, ServiceRequestEditModel serviceRequestModel)
        {
            ServiceRequest serviceRequest = await _serviceRequestsRepository.FindByIdAsync(id);
            if (serviceRequest is null)
            {
                return BadRequest();
            }

            _mapper.Map(serviceRequestModel, serviceRequest);
            _serviceRequestsRepository.Update(serviceRequest);

            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceRequestExists(id))
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

        // POST: api/ServiceRequests
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ServiceRequestViewModel>> PostServiceRequest([FromBody] ServiceRequestInputModel serviceRequestModel)
        {
            ServiceRequest serviceRequest = _mapper.Map<ServiceRequest>(serviceRequestModel);
            _serviceRequestsRepository.Insert(serviceRequest);

            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return _mapper.Map<ServiceRequestViewModel>(serviceRequest);
        }

        // DELETE: api/ServiceRequests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceRequestViewModel>> DeleteServiceRequest(int id)
        {
            ServiceRequest serviceRequest = await _serviceRequestsRepository.FindByIdAsync(id);
            if (serviceRequest == null)
            {
                return NotFound();
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
