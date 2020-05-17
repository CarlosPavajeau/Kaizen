using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesRepository _servicesRepository;
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public ServicesController(IServicesRepository servicesRepository, IUnitWork unitWork, IMapper mapper)
        {
            _servicesRepository = servicesRepository;
            _unitWork = unitWork;
            _mapper = mapper;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceViewModel>>> GetServices()
        {
            return await _servicesRepository.GetAll().Select(s => _mapper.Map<ServiceViewModel>(s)).ToListAsync();
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceViewModel>> GetService(string id)
        {
            Service service = await _servicesRepository.FindByIdAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return _mapper.Map<ServiceViewModel>(service);
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ServiceTypeViewModel>>> ServiceTypes()
        {
            IEnumerable<ServiceType> serviceTypes = await _servicesRepository.GetServiceTypesAsync();
            return Ok(_mapper.Map<IEnumerable<ServiceTypeViewModel>>(serviceTypes));
        }

        [HttpGet("[action]/{id}")]
        [AllowAnonymous]
        public async Task<bool> CheckExists(string id)
        {
            return await _servicesRepository.GetAll().AnyAsync(s => s.Code == id);
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(string id, ServiceEditModel serviceModel)
        {
            Service service = await _servicesRepository.FindByIdAsync(id);
            if (service is null)
            {
                return BadRequest();
            }

            _mapper.Map(serviceModel, service);
            _servicesRepository.Update(service);

            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // POST: api/Services
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ServiceViewModel>> PostService([FromBody]ServiceInputModel serviceModel)
        {
            Service service = _mapper.Map<Service>(serviceModel);
            _servicesRepository.Insert(service);
            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateException)
            {
                if (ServiceExists(service.Code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return _mapper.Map<ServiceViewModel>(service);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceViewModel>> DeleteService(string id)
        {
            Service service = await _servicesRepository.FindByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            return _mapper.Map<ServiceViewModel>(service);
        }

        private bool ServiceExists(string id)
        {
            return _servicesRepository.GetAll().Any(e => e.Code == id);
        }
    }
}
