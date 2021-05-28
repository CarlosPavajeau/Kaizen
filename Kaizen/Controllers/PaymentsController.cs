using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using Kaizen.Models.Base;
using Kaizen.Models.ProductInvoice;
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
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentClient _paymentClient;
        private readonly IProductInvoicesRepository _productInvoicesRepository;
        private readonly IServiceInvoicesRepository _serviceInvoicesRepository;
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public PaymentsController(PaymentClient paymentClient, IProductInvoicesRepository productInvoicesRepository,
            IServiceInvoicesRepository serviceInvoicesRepository, IUnitWork unitWork, IMapper mapper)
        {
            _paymentClient = paymentClient;
            _productInvoicesRepository = productInvoicesRepository;
            _serviceInvoicesRepository = serviceInvoicesRepository;
            _unitWork = unitWork;
            _mapper = mapper;
        }

        [HttpPost("[action]/{id:int}")]
        public async Task<ActionResult<ProductInvoiceViewModel>> PayProductInvoice(int id,
            [FromBody] PaymentModel paymentModel)
        {
            ProductInvoice productInvoice = await _productInvoicesRepository.FindByIdAsync(id);
            if (productInvoice is null)
            {
                return NotFound($"No existe ninguna factura de producto con el código {id}");
            }

            productInvoice.CalculateTotal();

            PaymentCreateRequest paymentCreateRequest = new PaymentCreateRequest
            {
                Token = paymentModel.Token,
                PaymentMethodId = paymentModel.PaymentMethodId,
                TransactionAmount = productInvoice.Total,
                Description = $"Pay for product invoice {id}",
                Installments = 1,
                Payer = new PaymentPayerRequest
                {
                    FirstName = productInvoice.Client.FirstName,
                    LastName = productInvoice.Client.LastName,
                    Email = paymentModel.Email
                }
            };

            Payment payment = await _paymentClient.CreateAsync(paymentCreateRequest);

            if (payment.Status == PaymentStatus.Rejected)
            {
                return BadRequest("El pago no pudo ser procesado.");
            }

            productInvoice.PublishEvent(new PaidInvoice(productInvoice));
            productInvoice.State = InvoiceState.Paid;
            productInvoice.PaymentDate = DateTime.Now;
            productInvoice.PaymentMethod = PaymentMethod.CreditCard;

            _productInvoicesRepository.Update(productInvoice);

            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductInvoiceExists(id))
                {
                    return NotFound(
                        $"Error de actualizacón. No existe ninguna factura de producto con el código {id}.");
                }

                throw;
            }

            return _mapper.Map<ProductInvoiceViewModel>(productInvoice);
        }

        private bool ProductInvoiceExists(int id)
        {
            return _productInvoicesRepository.GetAll().Any(p => p.Id == id);
        }

        [HttpPost("[action]/{id:int}")]
        public async Task<ActionResult<ServiceInvoiceViewModel>> PayServiceInvoice(int id,
            [FromBody] PaymentModel paymentModel)
        {
            ServiceInvoice serviceInvoice = await _serviceInvoicesRepository.FindByIdAsync(id);
            if (serviceInvoice is null)
            {
                return NotFound($"No existe ninguna factura de servicio con el código {id}.");
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
                    return NotFound(
                        $"Error de actualizacón. No existe ninguna factura de servicio con el código {id}.");
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
