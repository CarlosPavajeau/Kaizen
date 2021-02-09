using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Infrastructure.Test.Repositories
{
    [TestFixture]
    [Order(1)]
    public class EmployeesRepositoryTest : BaseRepositoryTest
    {
        private IEmployeesRepository _employeesRepository;
        private ApplicationDbContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            DetachAllEntities();

            _employeesRepository = ServiceProvider.GetService<IEmployeesRepository>();
            _dbContext = ServiceProvider.GetService<ApplicationDbContext>();
        }

        [Test]
        public void CheckEmployeesRepository()
        {
            Assert.IsNotNull(_employeesRepository);
        }

        [Test]
        public async Task Get_All_Employee_Charges()
        {
            try
            {
                List<EmployeeCharge> employeeCharges = await _employeesRepository.GetAllEmployeeCharges().ToListAsync();
                Assert.IsTrue(employeeCharges.Count > 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public async Task Save_Invalid_Employee()
        {
            try
            {
                Employee employee = new Employee();

                _employeesRepository.Insert(employee);

                await _dbContext.SaveChangesAsync();

                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task Save_Valid_Employee()
        {
            try
            {
                Employee employee = new Employee
                {
                    Id = "123456789",
                    FirstName = "Juan",
                    LastName = "Lopez",
                    ChargeId = 1, // Gerente
                    EmployeeContract = new EmployeeContract
                    {
                        ContractCode = "ADF22R",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddYears(1)
                    }
                };

                _employeesRepository.Insert(employee);

                await _dbContext.SaveChangesAsync();

                Assert.Pass();
            }
            catch (DbUpdateException e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public async Task Search_Non_Existent_Employee()
        {
            Employee employee = await _employeesRepository.FindByIdAsync("123456788");

            Assert.IsNull(employee);
        }

        [Test]
        public async Task Search_Existing_Employee()
        {
            Employee employee = await _employeesRepository.FindByIdAsync("123456789");

            Assert.IsNotNull(employee);
            Assert.AreEqual("123456789", employee.Id);
            Assert.AreEqual("Juan", employee.FirstName);
        }

        [Test]
        public async Task Update_Existing_Employee()
        {
            try
            {
                Employee employee = await _employeesRepository.FindByIdAsync("123456789");
                Assert.IsNotNull(employee);

                employee.LastName = "Manuel";
                _employeesRepository.Update(employee);

                await _dbContext.SaveChangesAsync();

                employee = await _employeesRepository.FindByIdAsync("123456789");
                Assert.AreEqual("Manuel", employee.LastName);
            }
            catch (DbUpdateException e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
