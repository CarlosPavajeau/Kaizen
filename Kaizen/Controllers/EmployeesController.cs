using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Core.Exceptions.Employee;
using Kaizen.Core.Exceptions.User;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.EditModels;
using Kaizen.InputModels;
using Kaizen.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> GetEmployees()
        {
            return await _employeesRepository.GetAll().Include(p => p.EmployeeCharge).Select(p => _mapper.Map<EmployeeViewModel>(p)).ToListAsync();
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EmployeeChargeViewModel>>> EmployeeCharges()
        {
            return await _employeesRepository.GetAllEmployeeCharges().Select(p => _mapper.Map<EmployeeChargeViewModel>(p)).ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployee(string id)
        {
            Employee employee = await _employeesRepository.FindByIdAsync(id);

            if (employee == null)
                throw new EmployeeDoesNotExists();

            return _mapper.Map<EmployeeViewModel>(employee);
        }

        [HttpGet("[action]/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> CheckExists(string id)
        {
            return await _employeesRepository.GetAll().AnyAsync(c => c.Id == id);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(string id, EmployeeEditModel employeeModel)
        {
            Employee employee = await _employeesRepository.FindByIdAsync(id);

            if (employee is null)
                throw new EmployeeDoesNotExists();

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
            ApplicationUser applicationUser = await _unitWork.ApplicationUsers.FindByIdAsync(employeeModel.UserId);
            if (applicationUser is null)
                throw new UserDoesNotExists();

            EmployeeCharge employeeCharge = await _employeesRepository.GetAllEmployeeCharges().Where(c => c.Id == employeeModel.ChargeId).FirstOrDefaultAsync();
            if (employeeCharge is null)
                throw new EmployeeChargeDoesNotExists();

            Employee employee = _mapper.Map<Employee>(employeeModel);
            employee.User = applicationUser;
            employee.EmployeeCharge = employeeCharge;

            _employeesRepository.Insert(employee);


            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.Id))
                {
                    throw new EmployeeAlreadyRegistered(employee.Id);
                }
                else
                {
                    throw new EmployeeNotRegister();
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
                throw new EmployeeDoesNotExists();

            await _unitWork.SaveAsync();

            return _mapper.Map<EmployeeViewModel>(employee);
        }

        private bool EmployeeExists(string id)
        {
            return _employeesRepository.GetAll().Any(p => p.Id == id);
        }
    }
}
