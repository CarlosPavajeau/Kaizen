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
    public class EquipmentsRepositoryTest : BaseRepositoryTest
    {
        private IEquipmentsRepository equipmentsRepository;
        private ApplicationDbContext dbContext;

        [SetUp]
        public void SetUp()
        {
            equipmentsRepository = ServiceProvider.GetService<IEquipmentsRepository>();
            dbContext = ServiceProvider.GetService<ApplicationDbContext>();
        }

        [Test]
        public void CheckEquipmentsRepository()
        {
            Assert.IsNotNull(equipmentsRepository);
        }

        [Test]
        public async Task SaveEquipment()
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

                equipmentsRepository.Insert(equipment);

                await dbContext.SaveChangesAsync();

                Assert.Pass();
            }
            catch (DbUpdateException)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task SearchEquipment()
        {
            try
            {
                Equipment equipment = await equipmentsRepository.FindByIdAsync("123RF");

                Assert.IsNotNull(equipment);
                Assert.AreEqual("123RF", equipment.Code);
                Assert.AreEqual("Rociador", equipment.Name);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task UpdateEquipment()
        {
            try
            {
                Equipment equipment = await equipmentsRepository.FindByIdAsync("123RF");
                Assert.IsNotNull(equipment);

                equipment.Name = "Rociador de pesticida";
                equipmentsRepository.Update(equipment);

                await dbContext.SaveChangesAsync();

                equipment = await equipmentsRepository.FindByIdAsync("123RF");
                Assert.AreEqual("Rociador de pesticida", equipment.Name);
            }
            catch (DbUpdateException)
            {
                Assert.Fail();
            }
        }
    }
}
