using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.ApplicationUser;
using Kaizen.Models.Employee;
using Kaizen.Test.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Kaizen.Test.Controllers
{
    [TestFixture]
    public class EmployeesControllerTest : BaseControllerTest
    {
        private EmployeesController _employeesController;
        private Mock<IEmployeesRepository> _employeesRepository;
        private Mock<IApplicationUserRepository> _applicationUserRepository;
        private Mock<IUnitWork> _unitWork;

        [SetUp]
        public void SetUp()
        {
            _employeesRepository = new Mock<IEmployeesRepository>();
            _applicationUserRepository = new Mock<IApplicationUserRepository>();
            _unitWork = new Mock<IUnitWork>();

            _employeesController = new EmployeesController(_employeesRepository.Object,
                _applicationUserRepository.Object, _unitWork.Object, ServiceProvider.GetService<IMapper>());
        }

        [Test]
        public async Task GetEmployees()
        {
            _employeesRepository.Setup(r => r.GetAll()).Returns(new TestAsyncEnumerable<Employee>(new[]
            {
                new Employee
                {
                    Id = "32752431",
                    FirstName = "Juan",
                    EmployeeCharge = new EmployeeCharge
                    {
                        Id = 1,
                        Charge = "Gerente"
                    }
                }
            }).AsQueryable());

            OkObjectResult result = (await _employeesController.GetEmployees()).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<EmployeeViewModel>>(result.Value);
        }

        [Test]
        public async Task GetTechnicians()
        {
            _employeesRepository.Setup(r => r.GetTechnicians()).ReturnsAsync(new[]
            {
                new Employee
                {
                    Id = "32752432",
                    FirstName = "Juan",
                    EmployeeCharge = new EmployeeCharge
                    {
                        Id = 2,
                        Charge = "Técnico"
                    }
                }
            });

            OkObjectResult result = (await _employeesController.Technicians()).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<EmployeeViewModel>>(result.Value);
        }

        [Test]
        public async Task GetTechniciansAvailable()
        {
            _employeesRepository.Setup(r => r.GetTechniciansAvailable(It.IsAny<DateTime>(), It.IsAny<string[]>()))
                .ReturnsAsync(new[]
                {
                    new Employee
                    {
                        Id = "32752432",
                        FirstName = "Juan",
                        EmployeeCharge = new EmployeeCharge
                        {
                            Id = 2,
                            Charge = "Técnico"
                        }
                    }
                });

            OkObjectResult result =
                (await _employeesController.TechniciansAvailable(DateTime.Now, "132,EER2")).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<EmployeeViewModel>>(result.Value);
        }

        [Test]
        public async Task GetEmployeeCharges()
        {
            _employeesRepository.Setup(r => r.GetAllEmployeeCharges()).Returns(new TestAsyncEnumerable<EmployeeCharge>(
                new[]
                {
                    new EmployeeCharge
                    {
                        Id = 1,
                        Charge = "Administrador"
                    }
                }).AsQueryable());

            OkObjectResult result = (await _employeesController.EmployeeCharges()).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<EmployeeChargeViewModel>>(result.Value);
        }

        [Test]
        public async Task GetEmployee()
        {
            _employeesRepository.Setup(r => r.FindByIdAsync("32752431")).ReturnsAsync(new Employee
            {
                Id = "32752431",
                FirstName = "Juan"
            });

            ActionResult<EmployeeViewModel> result = await _employeesController.GetEmployee("32752431");

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }

        [Test]
        public async Task CheckExists()
        {
            _employeesRepository.Setup(r => r.GetAll()).Returns(new TestAsyncEnumerable<Employee>(new[]
            {
                new Employee
                {
                    Id = "32752431",
                    FirstName = "Pedro"
                }
            }).AsQueryable());

            var result = await _employeesController.CheckExists("32752431");

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.IsTrue(result.Value);
        }

        [Test]
        public async Task PutEmployee()
        {
            _employeesRepository.Setup(r => r.FindByIdAsync("32752431")).ReturnsAsync(new Employee
            {
                Id = "32752431",
                FirstName = "Juan"
            });
            _employeesRepository.Setup(r => r.EmployeeContractAlreadyExists(It.IsAny<string>())).ReturnsAsync(true);
            _employeesRepository.Setup(r => r.Update(It.IsAny<Employee>())).Verifiable();

            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            ActionResult<EmployeeViewModel> result = await _employeesController.PutEmployee("32752431", new EmployeeEditModel
            {
                FirstName = "Pedro"
            });

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.AreEqual("Pedro", result.Value.FirstName);
        }

        [Test]
        public async Task PostEmployee()
        {
            _employeesRepository.Setup(r => r.GetAllEmployeeCharges()).Returns(new TestAsyncEnumerable<EmployeeCharge>(
                new[]
                {
                    new EmployeeCharge
                    {
                        Id = 1,
                        Charge = "Administrador"
                    }
                }).AsQueryable());
            _employeesRepository.Setup(r => r.Insert(It.IsAny<Employee>())).Verifiable();

            _applicationUserRepository.Setup(r => r.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _applicationUserRepository.Setup(r => r.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            ActionResult<EmployeeViewModel> result = await _employeesController.PostEmployee(new EmployeeInputModel
            {
                Id = "32752431",
                FirstName = "Pedro",
                ChargeId = 1,
                User = new ApplicationUserInputModel
                {
                    Username = "employee",
                    Password = "ThisIsASecurePassword"
                }
            });

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }
    }
}
