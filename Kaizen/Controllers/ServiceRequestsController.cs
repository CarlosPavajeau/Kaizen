using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
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
        public async Task<ActionResult<IEnumerable<ServiceRequest>>> GetServiceRequests()
        {
            return await _serviceRequestsRepository.GetAll().ToListAsync();
        }

        // GET: api/ServiceRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRequest>> GetServiceRequest(int id)
        {
            var serviceRequest = await _serviceRequestsRepository.FindByIdAsync(id);

            if (serviceRequest == null)
            {
                return NotFound();
            }

            return serviceRequest;
        }

        // PUT: api/ServiceRequests/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceRequest(int id, ServiceRequest serviceRequest)
        {
            if (id != serviceRequest.Code)
            {
                return BadRequest();
            }

            //_context.Entry(serviceRequest).State = EntityState.Modified;

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
        public async Task<ActionResult<ServiceRequest>> PostServiceRequest(ServiceRequest serviceRequest)
        {
            _serviceRequestsRepository.Insert(serviceRequest);
            await _unitWork.SaveAsync();

            return CreatedAtAction("GetServiceRequest", new { id = serviceRequest.Code }, serviceRequest);
        }

        // DELETE: api/ServiceRequests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceRequest>> DeleteServiceRequest(int id)
        {
            var serviceRequest = await _serviceRequestsRepository.FindByIdAsync(id);
            if (serviceRequest == null)
            {
                return NotFound();
            }

            _serviceRequestsRepository.Delete(serviceRequest);
            await _unitWork.SaveAsync();

            return serviceRequest;
        }

        private bool ServiceRequestExists(int id)
        {
            return _serviceRequestsRepository.GetAll().Any(s => s.Code == id);
        }
    }
}
