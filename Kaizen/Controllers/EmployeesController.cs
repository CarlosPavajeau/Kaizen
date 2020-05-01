using AutoMapper;
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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeesRepository employeesRepository, IUnitWork unitWork, IMapper mapper)
        {
            _employeesRepository = employeesRepository;
            _unitWork = unitWork;
            _mapper = mapper;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> GetEmployees()
        {
            return await _employeesRepository.GetAll().Select(p => _mapper.Map<EmployeeViewModel>(p)).ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployee(string id)
        {
            Employee employee = await _employeesRepository.FindByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return _mapper.Map<EmployeeViewModel>(employee);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(string id, EmployeeEditModel employeeModel)
        {
            Employee employee = await _employeesRepository.FindByIdAsync(id);

            if (employee is null)
            {
                return BadRequest();
            }

            _mapper.Map(employeeModel, employee);
            _employeesRepository.Update(employee);

            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EmployeeViewModel>> PostEmployee(EmployeeInputModel employeeModel)
        {
            Employee employee = _mapper.Map<Employee>(employeeModel);
            _employeesRepository.Insert(employee);

            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return _mapper.Map<EmployeeViewModel>(employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> DeleteEmployee(string id)
        {
            Employee employee = await _employeesRepository.FindByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _unitWork.SaveAsync();

            return _mapper.Map<EmployeeViewModel>(employee);
        }

        private bool EmployeeExists(string id)
        {
            return _employeesRepository.GetAll().Any(p => p.Id == id);
        }
    }
}
