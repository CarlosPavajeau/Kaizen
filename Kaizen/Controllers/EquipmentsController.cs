using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.Equipment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly IEquipmentsRepository _equipmentsRepository;
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public EquipmentsController(IEquipmentsRepository equipmentsRepository, IUnitWork unitWork, IMapper mapper)
        {
            _equipmentsRepository = equipmentsRepository;
            _unitWork = unitWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentViewModel>>> GetEquipments()
        {
            List<Equipment> equipments = await _equipmentsRepository.GetAll().ToListAsync();
            return Ok(_mapper.Map<IEnumerable<EquipmentViewModel>>(equipments));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentViewModel>> GetEquipment(string id)
        {
            Equipment equipment = await _equipmentsRepository.FindByIdAsync(id);
            if (equipment == null)
            {
                return NotFound($"No existe ningún equipo con el código {id}.");
            }

            return _mapper.Map<EquipmentViewModel>(equipment);
        }

        [HttpGet("[action]/{id}")]
        [AllowAnonymous]
        public async Task<bool> CheckExists(string id)
        {
            return await _equipmentsRepository.GetAll().AnyAsync(e => e.Code == id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EquipmentViewModel>> PutEquipment(string id, EquipmentEditModel equipmentModel)
        {
            Equipment equipment = await _equipmentsRepository.FindByIdAsync(id);
            if (equipment is null)
            {
                return BadRequest($"No existe ningún equipo con el código {id}.");
            }

            _mapper.Map(equipmentModel, equipment);
            _equipmentsRepository.Update(equipment);

            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentExists(id))
                {
                    return NotFound($"Actualizacón fallida. No existe ningún equipo con el código {id}.");
                }

                throw;
            }

            return _mapper.Map<EquipmentViewModel>(equipment);
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentViewModel>> PostEquipment(EquipmentInputModel equipmentModel)
        {
            Equipment equipment = _mapper.Map<Equipment>(equipmentModel);
            _equipmentsRepository.Insert(equipment);

            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateException)
            {
                if (EquipmentExists(equipment.Code))
                {
                    return Conflict($"Ya existe un equipo con el código {equipmentModel.Code}.");
                }

                throw;
            }

            return _mapper.Map<EquipmentViewModel>(equipment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EquipmentViewModel>> DeleteEquipment(string id)
        {
            Equipment equipment = await _equipmentsRepository.FindByIdAsync(id);
            if (equipment == null)
            {
                return NotFound($"No existe ningún equipo con el código {id}.");
            }

            return _mapper.Map<EquipmentViewModel>(equipment);
        }

        private bool EquipmentExists(string id)
        {
            return _equipmentsRepository.GetAll().Any(e => e.Code == id);
        }
    }
}
