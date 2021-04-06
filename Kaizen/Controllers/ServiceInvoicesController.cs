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
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Payment;
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
        private readonly PaymentClient _paymentClient;

        public ServiceInvoicesController(IServiceInvoicesRepository serviceInvoicesRepository, IUnitWork unitWork, IMapper mapper, PaymentClient paymentClient)
        {
            _serviceInvoicesRepository = serviceInvoicesRepository;
            _unitWork = unitWork;
            _mapper = mapper;
            _paymentClient = paymentClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceInvoiceViewModel>>> GetServiceInvoices()
        {
            List<ServiceInvoice> serviceInvoices = await _serviceInvoicesRepository.GetAll()
                .Include(s => s.Client)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ServiceInvoiceViewModel>>(serviceInvoices));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceInvoiceViewModel>> GetServiceInvoice(int id)
        {
            ServiceInvoice serviceInvoice = await _serviceInvoicesRepository.FindByIdAsync(id);
            if (serviceInvoice == null)
            {
                return NotFound($"No existe ninguna factura de servicio con el código { id }.");
            }

            return _mapper.Map<ServiceInvoiceViewModel>(serviceInvoice);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<IEnumerable<ServiceInvoiceDetailViewModel>>> ClientInvoices(string id)
        {
            IEnumerable<ServiceInvoice> serviceInvoices = await _serviceInvoicesRepository.GetClientInvoices(id);
            return Ok(_mapper.Map<IEnumerable<ServiceInvoiceViewModel>>(serviceInvoices));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceInvoiceViewModel>> PutServiceInvoice(int id, ServiceInvoiceEditModel serviceInvoiceModel)
        {
            ServiceInvoice serviceInvoice = await _serviceInvoicesRepository.FindByIdAsync(id);
            if (serviceInvoice is null)
            {
                return NotFound($"No existe ninguna factura de servicio con el código { id }.");
            }

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

                throw;
            }

            return _mapper.Map<ServiceInvoiceViewModel>(serviceInvoice);
        }

        [HttpPost("[action]/{id}")]
        public async Task<ActionResult<ServiceInvoiceViewModel>> Pay(int id, [FromBody] PaymentModel paymentModel)
        {
            ServiceInvoice serviceInvoice = await _serviceInvoicesRepository.FindByIdAsync(id);
            if (serviceInvoice is null)
            {
                return NotFound($"No existe ninguna factura de servicio con el código { id }.");
            }

            serviceInvoice.CalculateTotal();

            PaymentCreateRequest paymentCreateRequest = new PaymentCreateRequest
            {
                Token = paymentModel.Token,
                PaymentMethodId = paymentModel.PaymentMethodId,
                TransactionAmount = serviceInvoice.Total,
                Description = $"Pay of service invoice {serviceInvoice.Id}",
                Installments = 1,
                Payer = new PaymentPayerRequest
                {
                    FirstName = serviceInvoice.Client.FirstName,
                    LastName = serviceInvoice.Client.LastName,
                    Email = paymentModel.Email
                }
            };

            Payment payment = await _paymentClient.CreateAsync(paymentCreateRequest);

            if (payment.Status == PaymentStatus.Rejected)
            {
                return BadRequest("El pago no pudo ser procesado.");
            }

            serviceInvoice.State = InvoiceState.Paid;
            serviceInvoice.PaymentDate = DateTime.Now;
            serviceInvoice.PaymentMethod = PaymentMethod.CreditCard;

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

                throw;
            }

            return _mapper.Map<ServiceInvoiceViewModel>(serviceInvoice);
        }

        private bool ServiceInvoiceExists(int id)
        {
            return _serviceInvoicesRepository.GetAll().Any(s => s.Id == id);
        }
    }
}
