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
    public class EmployeesRepositoryTest : BaseRepositoryTest
    {
        private IEmployeesRepository employeesRepository;
        private ApplicationDbContext dbContext;

        [SetUp]
        public void SetUp()
        {
            DetachAllEntities();

            employeesRepository = ServiceProvider.GetService<IEmployeesRepository>();
            dbContext = ServiceProvider.GetService<ApplicationDbContext>();
        }

        [Test]
        public void CheckEmployeesRepository()
        {
            Assert.IsNotNull(employeesRepository);
        }

        [Test]
        public async Task LoadEmployeeCharges()
        {
            try
            {
                List<EmployeeCharge> employeeCharges = await employeesRepository.GetAllEmployeeCharges().ToListAsync();
                Assert.IsTrue(employeeCharges.Count > 0);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task SaveEmployee()
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

                employeesRepository.Insert(employee);

                await dbContext.SaveChangesAsync();

                Assert.Pass();
            }
            catch (DbUpdateException)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task SearchEmployee()
        {
            try
            {
                Employee employee = await employeesRepository.FindByIdAsync("123456789");

                Assert.IsNotNull(employee);
                Assert.AreEqual("123456789", employee.Id);
                Assert.AreEqual("Juan", employee.FirstName);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task UpdateEmployee()
        {
            try
            {
                Employee employee = await employeesRepository.FindByIdAsync("123456789");
                Assert.IsNotNull(employee);

                employee.LastName = "Manuel";
                employeesRepository.Update(employee);

                await dbContext.SaveChangesAsync();

                employee = await employeesRepository.FindByIdAsync("123456789");
                Assert.AreEqual("Manuel", employee.LastName);
            }
            catch (DbUpdateException)
            {
                Assert.Fail();
            }
        }
    }
}
