using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using Kaizen.Models.Base;
using Kaizen.Models.ServiceInvoice;
using MercadoPagoCore.Common;
using MercadoPagoCore.Resources;
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
                return NotFound($"No existe ninguna factura de servicio con el código { id }.");

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
                return NotFound($"No existe ninguna factura de servicio con el código { id }.");

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
                    return NotFound($"Error de actualizacón. No existe ninguna factura de servicio con el código {id}.");
                }
                else
                {
                    throw;
                }
            }

            return _mapper.Map<ServiceInvoiceViewModel>(serviceInvoice);
        }

        [HttpPost("[action]/{id}")]
        public async Task<ActionResult<ServiceInvoiceViewModel>> Pay(int id, [FromBody] PaymentModel paymentModel)
        {
            ServiceInvoice serviceInvoice = await _serviceInvoicesRepository.FindByIdAsync(id);
            if (serviceInvoice is null)
                return NotFound($"No existe ninguna factura de servicio con el código { id }.");

            serviceInvoice.CalculateTotal();

            Payment payment = new Payment()
            {
                Token = paymentModel.Token,
                PaymentMethodId = paymentModel.PaymentMethodId,
                TransactionAmount = (float?)serviceInvoice.Total,
                Description = $"Pay of service invoice {serviceInvoice.Id}",
                Installments = 1,
                Payer = new MercadoPagoCore.DataStructures.Payment.Payer
                {
                    FirstName = serviceInvoice.Client.FirstName,
                    LastName = serviceInvoice.Client.LastName,
                    Email = paymentModel.Email
                }
            };

            if (payment.Save())
            {
                if (payment.Status == PaymentStatus.approved)
                {
                    serviceInvoice.State = InvoiceState.Paid;
                    serviceInvoice.PaymentDate = DateTime.Now;
                    serviceInvoice.PaymentMethod = Domain.Entities.PaymentMethod.CreditCard;

                    serviceInvoice.PublishEvent(new PaidInvoice(serviceInvoice));
                    _serviceInvoicesRepository.Update(serviceInvoice);

                    try
                    {
                        await _unitWork.SaveAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ServiceInvoiceExists(id))
                        {
                            return NotFound($"Error de actualizacón. No existe ninguna factura de servicio con el código {id}.");
                        }
                        else
                        {
                            throw;
                        }
                    }

                    return _mapper.Map<ServiceInvoiceViewModel>(serviceInvoice);
                }
            }

            return BadRequest(payment.Errors.Value);
        }

        private bool ServiceInvoiceExists(int id)
        {
            return _serviceInvoicesRepository.GetAll().Any(s => s.Id == id);
        }
    }
}
