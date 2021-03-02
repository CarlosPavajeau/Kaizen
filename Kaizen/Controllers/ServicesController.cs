using System;
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ServiceViewModel>>> GetServices()
        {
            List<Service> services = await _servicesRepository.GetAll()
                .Include(s => s.ServiceType).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ServiceViewModel>>(services));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceViewModel>> GetService(string id)
        {
            Service service = await _servicesRepository.FindByIdAsync(id);
            if (service == null)
            {
                return NotFound($"No existe ningún servicio con el código {id}.");
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

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceViewModel>> PutService(string id, ServiceEditModel serviceModel)
        {
            Service service = await _servicesRepository.FindByIdAsync(id);
            if (service is null)
            {
                return BadRequest($"No existe ningún servicio con el código {id}.");
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
                    return NotFound($"Actualización fallida. No existe ningún servicio con el código {id}.");
                }

                throw;
            }

            return _mapper.Map<ServiceViewModel>(service);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<ServiceViewModel>> PostService([FromBody] ServiceInputModel serviceModel)
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
                    return Conflict($"Ya existe un servicio con el código {serviceModel.Code}.");
                }

                throw;
            }

            return _mapper.Map<ServiceViewModel>(service);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("[action]")]
        public async Task<ActionResult<ServiceTypeViewModel>> ServiceTypes([FromBody] ServiceTypeInputModel serviceTypeInputModel)
        {
            ServiceType serviceType = _mapper.Map<ServiceType>(serviceTypeInputModel);

            _servicesRepository.Insert(serviceType);

            await _unitWork.SaveAsync();

            return _mapper.Map<ServiceTypeViewModel>(serviceType);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceViewModel>> DeleteService(string id)
        {
            Service service = await _servicesRepository.FindByIdAsync(id);
            if (service == null)
            {
                return NotFound($"No existe ningún servicio con el código {id}.");
            }

            return _mapper.Map<ServiceViewModel>(service);
        }

        private bool ServiceExists(string id)
        {
            return _servicesRepository.GetAll().Any(e => e.Code == id);
        }
    }
}
