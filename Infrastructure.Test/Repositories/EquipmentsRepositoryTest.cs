using System;
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
    [Order(2)]
    public class EquipmentsRepositoryTest : BaseRepositoryTest
    {
        private IEquipmentsRepository _equipmentsRepository;
        private ApplicationDbContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            DetachAllEntities();

            _equipmentsRepository = ServiceProvider.GetService<IEquipmentsRepository>();
            _dbContext = ServiceProvider.GetService<ApplicationDbContext>();
        }

        [Test]
        public void CheckEquipmentsRepository()
        {
            Assert.IsNotNull(_equipmentsRepository);
        }

        [Test]
        public async Task Save_Invalid_Equipment()
        {
            try
            {
                Equipment equipment = new Equipment();

                _equipmentsRepository.Insert(equipment);

                await _dbContext.SaveChangesAsync();

                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task Save_Valid_Equipment()
        {
            try
            {
                Equipment equipment = new Equipment
                {
                    Code = "123RF",
                    Name = "Rociador",
                    MaintenanceDate = DateTime.Now,
                    Description = "Rociador de pesticida",
                    Amount = 30,
                    Price = 45000
                };

                _equipmentsRepository.Insert(equipment);

                await _dbContext.SaveChangesAsync();

                Assert.Pass();
            }
            catch (DbUpdateException)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task Search_Non_Existent_Equipment()
        {
            Equipment equipment = await _equipmentsRepository.FindByIdAsync("123FF");

            Assert.IsNull(equipment);
        }

        [Test]
        public async Task Search_Existing_Equipment()
        {
            Equipment equipment = await _equipmentsRepository.FindByIdAsync("123RF");

            Assert.IsNotNull(equipment);
            Assert.AreEqual("123RF", equipment.Code);
            Assert.AreEqual("Rociador", equipment.Name);
        }

        [Test]
        public async Task Update_Existing_Equipment()
        {
            try
            {
                Equipment equipment = await _equipmentsRepository.FindByIdAsync("123RF");
                Assert.IsNotNull(equipment);

                equipment.Name = "Rociador de pesticida";
                _equipmentsRepository.Update(equipment);

                await _dbContext.SaveChangesAsync();

                equipment = await _equipmentsRepository.FindByIdAsync("123RF");
                Assert.AreEqual("Rociador de pesticida", equipment.Name);
            }
            catch (DbUpdateException e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
