using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.WorkOrder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrdersController : ControllerBase
    {
        private readonly IWorkOrdersRepository _workOrdersRepository;
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public WorkOrdersController(IWorkOrdersRepository workOrdersRepository, IUnitWork unitWork, IMapper mapper)
        {
            _workOrdersRepository = workOrdersRepository;
            _unitWork = unitWork;
            _mapper = mapper;
        }

        // GET: api/WorkOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkOrderViewModel>>> GetWorkOrders()
        {
            List<WorkOrder> workOrders = await _workOrdersRepository.GetAll().ToListAsync();
            return Ok(_mapper.Map<IEnumerable<WorkOrderViewModel>>(workOrders));
        }

        // GET: api/WorkOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkOrderViewModel>> GetWorkOrder(int id)
        {
            WorkOrder workOrder = await _workOrdersRepository.FindByIdAsync(id);

            if (workOrder == null)
            {
                return NotFound();
            }

            return _mapper.Map<WorkOrderViewModel>(workOrder);
        }

        // PUT: api/WorkOrders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkOrder(int id, WorkOrderEditModel workOrderModel)
        {
            WorkOrder workOrder = await _workOrdersRepository.FindByIdAsync(id);
            if (workOrder is null)
            {
                return BadRequest();
            }

            _mapper.Map(workOrderModel, workOrder);
            _workOrdersRepository.Update(workOrder);

            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkOrderExists(id))
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

        // POST: api/WorkOrders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WorkOrderViewModel>> PostWorkOrder(WorkOrderInputModel workOrderModel)
        {
            WorkOrder workOrder = _mapper.Map<WorkOrder>(workOrderModel);
            _workOrdersRepository.Insert(workOrder);
            await _unitWork.SaveAsync();

            return _mapper.Map<WorkOrderViewModel>(workOrder);
        }

        // DELETE: api/WorkOrders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WorkOrderViewModel>> DeleteWorkOrder(int id)
        {
            var workOrder = await _workOrdersRepository.FindByIdAsync(id);
            if (workOrder == null)
            {
                return NotFound();
            }

            _workOrdersRepository.Delete(workOrder);
            await _unitWork.SaveAsync();

            return _mapper.Map<WorkOrderViewModel>(workOrder);
        }

        private bool WorkOrderExists(int id)
        {
            return _workOrdersRepository.GetAll().Any(w => w.Code == id);
        }
    }
}
