using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Extensions;
using Kaizen.Models.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeesRepository employeesRepository, IApplicationUserRepository applicationUserRepository, IUnitWork unitWork, IMapper mapper)
        {
            _employeesRepository = employeesRepository;
            _applicationUserRepository = applicationUserRepository;
            _unitWork = unitWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> GetEmployees()
        {
            List<Employee> employees = await _employeesRepository.GetAll()
                .Include(e => e.EmployeeCharge).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<EmployeeViewModel>>(employees));
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> Technicians()
        {
            IEnumerable<Employee> employees = await _employeesRepository.GetTechnicians();
            return Ok(_mapper.Map<IEnumerable<EmployeeViewModel>>(employees));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> TechniciansAvailable([FromQuery] DateTime date, [FromQuery] string serviceCodes)
        {
            IEnumerable<Employee> techniciansAvailable = await _employeesRepository.GetTechniciansAvailable(date, serviceCodes.Split(','));
            return Ok(_mapper.Map<IEnumerable<EmployeeViewModel>>(techniciansAvailable));
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EmployeeChargeViewModel>>> EmployeeCharges()
        {
            List<EmployeeCharge> employeeCharges = await _employeesRepository.GetAllEmployeeCharges().ToListAsync();
            return Ok(_mapper.Map<IEnumerable<EmployeeChargeViewModel>>(employeeCharges));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployee(string id)
        {
            Employee employee = await _employeesRepository.FindByIdAsync(id);
            if (employee == null)
            {
                return NotFound($"No existe ningún empleado registrado con el código {id}.");
            }

            return _mapper.Map<EmployeeViewModel>(employee);
        }

        [HttpGet("[action]/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> CheckExists(string id)
        {
            return await _employeesRepository.GetAll().AnyAsync(c => c.Id == id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> PutEmployee(string id, EmployeeEditModel employeeModel)
        {
            Employee employee = await _employeesRepository.FindByIdAsync(id);
            if (employee is null)
            {
                return BadRequest($"No existe ningún empleado registrado con el código {id}.");
            }

            bool employeeContractAlreadyExists = await _employeesRepository.EmployeeContractAlreadyExists(employeeModel.ContractCode);
            if (!employeeContractAlreadyExists)
            {
                EmployeeContract contract = _mapper.Map<EmployeeContract>(employeeModel.EmployeeContract);
                _employeesRepository.AddNewEmployeeContract(contract);
                employee.EmployeeContract = contract;
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
                    return NotFound($"Actualización fallida. No existe ningún empleado registrado con el código {id}.");
                }
                else
                {
                    throw;
                }
            }

            return _mapper.Map<EmployeeViewModel>(employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeViewModel>> PostEmployee(EmployeeInputModel employeeModel)
        {
            EmployeeCharge employeeCharge = await _employeesRepository.GetAllEmployeeCharges()
                .FirstOrDefaultAsync(c => c.Id == employeeModel.ChargeId);
            if (employeeCharge is null)
            {
                return BadRequest("El cargo del empleado no se encuentra registrado.");
            }

            Employee employee = _mapper.Map<Employee>(employeeModel);
            employee.EmployeeCharge = employeeCharge;

            IdentityResult result = await _applicationUserRepository.CreateAsync(employee.User, employeeModel.User.Password);
            if (!result.Succeeded)
            {
                return this.IdentityResultErrors(result);
            }

            IdentityResult roleResult = await _applicationUserRepository.AddToRoleAsync(employee.User, GetEmployeeRole(employee));
            if (!roleResult.Succeeded)
            {
                return this.IdentityResultErrors(roleResult);
            }

            _employeesRepository.Insert(employee);

            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.Id))
                {
                    return Conflict($"Ya existe un empleado registrado con el código {employeeModel.Id}");
                }
                else
                {
                    throw;
                }
            }

            return _mapper.Map<EmployeeViewModel>(employee);
        }

        private static string GetEmployeeRole(Employee employee)
        {
            return employee.EmployeeCharge.Id switch
            {
                1 => "Administrator",
                5 => "OfficeEmployee",
                6 or 7 => "TechnicalEmployee",
                _ => "Employee",
            };
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> DeleteEmployee(string id)
        {
            Employee employee = await _employeesRepository.FindByIdAsync(id);
            if (employee == null)
            {
                return NotFound($"No existe ningún empleado registrado con el código {id}.");
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
