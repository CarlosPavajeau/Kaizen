using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.ServiceInvoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceInvoicesController : ControllerBase
    {
        private readonly IServiceInvoicesRepository _serviceInvoicesRepository;
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public ServiceInvoicesController(IServiceInvoicesRepository serviceInvoicesRepository, IUnitWork unitWork, IMapper mapper)
        {
            _serviceInvoicesRepository = serviceInvoicesRepository;
            _unitWork = unitWork;
            _mapper = mapper;
        }

        // GET: api/ServiceInvoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceInvoiceViewModel>>> GetServiceInvoices()
        {
            List<ServiceInvoice> serviceInvoices = await _serviceInvoicesRepository.GetAll()
                .Include(s => s.Client)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ServiceInvoiceViewModel>>(serviceInvoices));
        }

        // GET: api/ServiceInvoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceInvoiceViewModel>> GetServiceInvoice(int id)
        {
            ServiceInvoice serviceInvoice = await _serviceInvoicesRepository.FindByIdAsync(id);

            if (serviceInvoice == null)
                return NotFound();

            return _mapper.Map<ServiceInvoiceViewModel>(serviceInvoice);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<IEnumerable<ServiceInvoiceDetailViewModel>>> ClientInvoices(string id)
        {
            IEnumerable<ServiceInvoice> serviceInvoices = await _serviceInvoicesRepository.GetClientInvoices(id);
            return Ok(_mapper.Map<IEnumerable<ServiceInvoiceViewModel>>(serviceInvoices));
        }

        // PUT: api/ServiceInvoices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceInvoiceViewModel>> PutServiceInvoice(int id, ServiceInvoiceEditModel serviceInvoiceModel)
        {
            ServiceInvoice serviceInvoice = await _serviceInvoicesRepository.FindByIdAsync(id);
            if (serviceInvoice is null)
                return NotFound();

            _mapper.Map(serviceInvoiceModel, serviceInvoice);
            _serviceInvoicesRepository.Update(serviceInvoice);

            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceInvoiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return _mapper.Map<ServiceInvoiceViewModel>(serviceInvoice);
        }

        private bool ServiceInvoiceExists(int id)
        {
            return _serviceInvoicesRepository.GetAll().Any(s => s.Id == id);
        }
    }
}
