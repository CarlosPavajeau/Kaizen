using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.ProductInvoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductInvoicesController : ControllerBase
    {
        private readonly IProductInvoicesRepository _productInvoicesRepository;
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public ProductInvoicesController(IProductInvoicesRepository productInvoicesRepository, IUnitWork unitWork,
            IMapper mapper)
        {
            _productInvoicesRepository = productInvoicesRepository;
            _unitWork = unitWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInvoiceViewModel>>> GetProductInvoices()
        {
            List<ProductInvoice> productInvoices = await _productInvoicesRepository.GetAll().ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ProductInvoiceViewModel>>(productInvoices));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInvoiceViewModel>> GetProductInvoice(int id)
        {
            ProductInvoice productInvoice = await _productInvoicesRepository.FindByIdAsync(id);

            if (productInvoice == null)
            {
                return NotFound($"No existe ninguna factura de productos con el código {id}.");
            }

            return _mapper.Map<ProductInvoiceViewModel>(productInvoice);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<IEnumerable<ProductInvoiceViewModel>>> ClientInvoices(string id)
        {
            IEnumerable<ProductInvoice> productInvoices = await _productInvoicesRepository.GetClientInvoices(id);
            return Ok(_mapper.Map<IEnumerable<ProductInvoiceViewModel>>(productInvoices));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductInvoiceViewModel>> PutProductInvoice(int id,
            ProductInvoiceEditModel productInvoiceModel)
        {
            ProductInvoice productInvoice = await _productInvoicesRepository.FindByIdAsync(id);

            if (productInvoice == null)
            {
                return BadRequest($"No existe ninguna factura de productos con el código {id}.");
            }

            _mapper.Map(productInvoiceModel, productInvoice);
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
                        $"Error de actialización. No existe ninguna factura de productos con el código {id}.");
                }

                throw;
            }

            return _mapper.Map<ProductInvoiceViewModel>(productInvoice);
        }

        [HttpPost]
        public async Task<ActionResult<ProductInvoiceViewModel>> PostProductInvoice(
            ProductInvoiceInputModel productInvoiceModel)
        {
            ProductInvoice productInvoice = _mapper.Map<ProductInvoice>(productInvoiceModel);
            await _productInvoicesRepository.Insert(productInvoice);

            await _unitWork.SaveAsync();

            return _mapper.Map<ProductInvoiceViewModel>(productInvoice);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductInvoiceViewModel>> DeleteProductInvoice(int id)
        {
            ProductInvoice productInvoice = await _productInvoicesRepository.FindByIdAsync(id);
            if (productInvoice == null)
            {
                return NotFound($"No existe ninguna factura de productos con el código {id}.");
            }

            _productInvoicesRepository.Delete(productInvoice);
            await _unitWork.SaveAsync();

            return _mapper.Map<ProductInvoiceViewModel>(productInvoice);
        }

        private bool ProductInvoiceExists(int id)
        {
            return _productInvoicesRepository.GetAll().Any(p => p.Id == id);
        }
    }
}
