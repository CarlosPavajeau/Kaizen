using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using Kaizen.Models.WorkOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkOrderViewModel>>> GetWorkOrders()
        {
            List<WorkOrder> workOrders = await _workOrdersRepository.GetAll().ToListAsync();
            return Ok(_mapper.Map<IEnumerable<WorkOrderViewModel>>(workOrders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkOrderViewModel>> GetWorkOrder(int id)
        {
            WorkOrder workOrder = await _workOrdersRepository.FindByIdAsync(id);
            if (workOrder == null)
            {
                return NotFound($"La orden de trabajo con el código { id } no se encuentra registrada.");
            }

            return _mapper.Map<WorkOrderViewModel>(workOrder);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<WorkOrderViewModel>> ActivityWorkOrder(int id)
        {
            WorkOrder workOrder = await _workOrdersRepository.FindByActivityCodeAsync(id);
            if (workOrder == null)
            {
                return NotFound($"No existe una orden de trabajo asosiada a la actividad con código { id }");
            }

            return _mapper.Map<WorkOrderViewModel>(workOrder);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<SectorViewModel>> Sectors()
        {
            IEnumerable<Sector> sectors = await _workOrdersRepository.GetSectorsAsync();
            return Ok(_mapper.Map<IEnumerable<SectorViewModel>>(sectors));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WorkOrderViewModel>> PutWorkOrder(int id, WorkOrderEditModel workOrderModel)
        {
            WorkOrder workOrder = await _workOrdersRepository.FindByIdAsync(id);
            if (workOrder is null)
            {
                return BadRequest($"No se pudo actualizar la orden de trabjo con código { id }. Verifique la existencia de esta.");
            }

            _mapper.Map(workOrderModel, workOrder);
            _workOrdersRepository.Update(workOrder);
            workOrder.PublishEvent(new UpdatedWorkOrder(workOrder));

            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkOrderExists(id))
                {
                    return NotFound($"No existe una orden de trabajo asosiada a la actividad con código { id }");
                }
                else
                {
                    throw;
                }
            }

            return _mapper.Map<WorkOrderViewModel>(workOrder);
        }

        [HttpPost]
        public async Task<ActionResult<WorkOrderViewModel>> PostWorkOrder(WorkOrderInputModel workOrderModel)
        {
            WorkOrder workOrder = _mapper.Map<WorkOrder>(workOrderModel);

            _workOrdersRepository.Insert(workOrder);
            await _unitWork.SaveAsync();

            return _mapper.Map<WorkOrderViewModel>(workOrder);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<WorkOrderViewModel>> DeleteWorkOrder(int id)
        {
            WorkOrder workOrder = await _workOrdersRepository.FindByIdAsync(id);
            if (workOrder == null)
            {
                return NotFound($"No se pudo eliminar la orden de trabjo con código { id }. Verifique la existencia de esta.");
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
