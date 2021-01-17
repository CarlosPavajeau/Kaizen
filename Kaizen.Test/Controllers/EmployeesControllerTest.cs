using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Infrastructure.Identity;
using Kaizen.Models.ApplicationUser;
using Kaizen.Models.Employee;
using Kaizen.Test.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            SetUpEmployeesRepository();
            SetUpApplicationUserRepository();
            SetUpUnitWork();
        }

        private void SetUpEmployeesRepository()
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

            _employeesRepository.Setup(r => r.GetAllEmployeeCharges()).Returns(new TestAsyncEnumerable<EmployeeCharge>(
                new[]
                {
                    new EmployeeCharge
                    {
                        Id = 1,
                        Charge = "Administrador"
                    }
                }).AsQueryable());

            _employeesRepository.Setup(r => r.FindByIdAsync("32752431")).ReturnsAsync(new Employee
            {
                Id = "32752431",
                FirstName = "Juan"
            });
            _employeesRepository.Setup(r => r.FindByIdAsync("32752432")).ReturnsAsync((Employee)null);

            _employeesRepository.Setup(r => r.EmployeeContractAlreadyExists(It.IsAny<string>())).ReturnsAsync(true);

            _employeesRepository.Setup(r => r.Update(It.IsAny<Employee>())).Verifiable();
            _employeesRepository.Setup(r => r.Insert(It.IsAny<Employee>())).Verifiable();
        }

        private void SetUpApplicationUserRepository()
        {

            _applicationUserRepository.Setup(r => r.CreateAsync(It.Is<ApplicationUser>(a => a.UserName == "employee"),
                    It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _applicationUserRepository.Setup(r => r.CreateAsync(It.Is<ApplicationUser>(a => a.UserName == "admin"),
                    It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new SpanishIdentityErrorDescriber().DuplicateUserName("admin")));

            _applicationUserRepository.Setup(r => r.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
        }

        private void SetUpUnitWork()
        {
            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);
        }

        [Test]
        public async Task Get_All_Employees()
        {
            OkObjectResult result = (await _employeesController.GetEmployees()).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<EmployeeViewModel>>(result.Value);
        }

        [Test]
        public async Task Get_All_Technicians()
        {
            OkObjectResult result = (await _employeesController.Technicians()).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<EmployeeViewModel>>(result.Value);
        }

        [Test]
        public async Task Get_All_Technicians_Available()
        {
            OkObjectResult result =
                (await _employeesController.TechniciansAvailable(DateTime.Now, "132,EER2")).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<EmployeeViewModel>>(result.Value);
        }

        [Test]
        public async Task Get_All_Employee_Charges()
        {
            OkObjectResult result = (await _employeesController.EmployeeCharges()).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<EmployeeChargeViewModel>>(result.Value);
        }

        [Test]
        public async Task Get_Existing_Employee()
        {
            ActionResult<EmployeeViewModel> result = await _employeesController.GetEmployee("32752431");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task Get_Non_Existent_Employee()
        {
            ActionResult<EmployeeViewModel> result = await _employeesController.GetEmployee("32752432");

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task Check_If_Employee_Exists()
        {
            ActionResult<bool> result = await _employeesController.CheckExists("32752431");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.Value);
        }

        [Test]
        public async Task Update_Existing_Employee()
        {
            ActionResult<EmployeeViewModel> result = await _employeesController.PutEmployee("32752431", new EmployeeEditModel
            {
                FirstName = "Pedro"
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual("Pedro", result.Value.FirstName);
        }

        [Test]
        public async Task Update_Non_Existent_Employee()
        {
            ActionResult<EmployeeViewModel> result = await _employeesController.PutEmployee("32752432", new EmployeeEditModel
            {
                FirstName = "Pedro"
            });

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Save_New_Employee_With_New_Username()
        {
            ActionResult<EmployeeViewModel> result = await _employeesController.PostEmployee(new EmployeeInputModel
            {
                Id = "32752432",
                FirstName = "Pedro",
                ChargeId = 1,
                User = new ApplicationUserInputModel
                {
                    Username = "employee",
                    Password = "ThisIsASecurePassword"
                }
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task Save_New_Employee_With_An_Existing_Username()
        {
            ActionResult<EmployeeViewModel> result = await _employeesController.PostEmployee(new EmployeeInputModel
            {
                Id = "32752432",
                FirstName = "Pedro",
                ChargeId = 1,
                User = new ApplicationUserInputModel
                {
                    Username = "admin",
                    Password = "ThisIsASecurePassword"
                }
            });

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Save_Existing_Employee()
        {
            _unitWork.Setup(r => r.SaveAsync()).Throws(new DbUpdateException());

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

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<ConflictObjectResult>(result.Result);
        }
    }
}
