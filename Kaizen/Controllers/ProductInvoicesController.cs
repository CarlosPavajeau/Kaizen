using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.ProductInvoice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInvoicesController : ControllerBase
    {
        private readonly IProductInvoicesRepository _productInvoicesRepository;
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public ProductInvoicesController(IProductInvoicesRepository productInvoicesRepository, IUnitWork unitWork, IMapper mapper)
        {
            _productInvoicesRepository = productInvoicesRepository;
            _unitWork = unitWork;
            _mapper = mapper;
        }

        // GET: api/ProductInvoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInvoiceViewModel>>> GetProductInvoices()
        {
            List<ProductInvoice> productInvoices = await _productInvoicesRepository.GetAll().ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ProductInvoiceDetailViewModel>>(productInvoices));
        }

        // GET: api/ProductInvoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInvoiceViewModel>> GetProductInvoice(int id)
        {
            ProductInvoice productInvoice = await _productInvoicesRepository.FindByIdAsync(id);

            if (productInvoice == null)
                return NotFound($"No existe ninguna factura de productos con el código { id }.");

            return _mapper.Map<ProductInvoiceViewModel>(productInvoice);
        }

        // PUT: api/ProductInvoices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductInvoiceViewModel>> PutProductInvoice(int id, ProductInvoiceEditModel productInvoiceModel)
        {
            ProductInvoice productInvoice = await _productInvoicesRepository.FindByIdAsync(id);

            if (productInvoice == null)
                return BadRequest($"No existe ninguna factura de productos con el código { id }.");

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
                    return NotFound($"Error de actialización. No existe ninguna factura de productos con el código { id }.");
                }
                else
                {
                    throw;
                }
            }

            return _mapper.Map<ProductInvoiceViewModel>(productInvoice);
        }

        // POST: api/ProductInvoices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProductInvoiceViewModel>> PostProductInvoice(ProductInvoiceInputModel productInvoiceModel)
        {
            ProductInvoice productInvoice = _mapper.Map<ProductInvoice>(productInvoiceModel);
            _productInvoicesRepository.Insert(productInvoice);

            await _unitWork.SaveAsync();

            return _mapper.Map<ProductInvoiceViewModel>(productInvoice);
        }

        // DELETE: api/ProductInvoices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductInvoiceViewModel>> DeleteProductInvoice(int id)
        {
            var productInvoice = await _productInvoicesRepository.FindByIdAsync(id);
            if (productInvoice == null)
                return NotFound($"No existe ninguna factura de productos con el código { id }.");

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
