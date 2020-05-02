using AutoMapper;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.EditModels;
using Kaizen.InputModels;
using Kaizen.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

		// GET: api/Equipments
		[HttpGet]
		public async Task<ActionResult<IEnumerable<EquipmentViewModel>>> GetEquipments()
		{
			return await _equipmentsRepository.GetAll().Select(e => _mapper.Map<EquipmentViewModel>(e)).ToListAsync();
		}

		// GET: api/Equipments/5
		[HttpGet("{id}")]
		public async Task<ActionResult<EquipmentViewModel>> GetEquipment(string id)
		{
			Equipment equipment = await _equipmentsRepository.FindByIdAsync(id);

			if (equipment == null)
			{
				return NotFound();
			}

			return _mapper.Map<EquipmentViewModel>(equipment);
		}

		[HttpGet("[action]/{id}")]
        [AllowAnonymous]
		public async Task<bool> CheckEquipmentExists(string id)
		{
			return await _equipmentsRepository.GetAll().AnyAsync(e => e.Code == id);
		}

		// PUT: api/Equipments/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPut("{id}")]
		public async Task<IActionResult> PutEquipment(string id, EquipmentEditModel equipmentModel)
		{
			Equipment equipment = await _equipmentsRepository.FindByIdAsync(id);
			if (equipment is null)
			{
				return BadRequest();
			}

			_equipmentsRepository.Update(equipment);

			try
			{
				await _unitWork.SaveAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!EquipmentExists(id))
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

		// POST: api/Equipments
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
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
					return Conflict();
				}
				else
				{
					throw;
				}
			}

			return _mapper.Map<EquipmentViewModel>(equipment);
		}

		// DELETE: api/Equipments/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<EquipmentViewModel>> DeleteEquipment(string id)
		{
			Equipment equipment = await _equipmentsRepository.FindByIdAsync(id);
			if (equipment == null)
			{
				return NotFound();
			}

			return _mapper.Map<EquipmentViewModel>(equipment);
		}

		private bool EquipmentExists(string id)
		{
			return _equipmentsRepository.GetAll().Any(e => e.Code == id);
		}
	}
}
