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
    [TestFixture, Order(7)]
    public class ActivitiesRepositoryTest : BaseRepositoryTest
    {
        private IActivitiesRepository _activitiesRepository;
        private ApplicationDbContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            _activitiesRepository = ServiceProvider.GetService<IActivitiesRepository>();
            _dbContext = ServiceProvider.GetService<ApplicationDbContext>();
        }

        [Test]
        public void CheckActivitiesRepository()
        {
            Assert.IsNotNull(_activitiesRepository);
        }

        [Test]
        public async Task Save_Valid_Activity()
        {
            try
            {
                Activity activity = new Activity
                {
                    Date = DateTime.Now,
                    State = ActivityState.Pending,
                    ClientId = "12345678",
                    Periodicity = PeriodicityType.Casual
                };

                _activitiesRepository.Insert(activity);

                await _dbContext.SaveChangesAsync();

                Assert.Pass();
            }
            catch (DbUpdateException)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task Save_Invalid_Activity()
        {
            try
            {
                Activity activity = new Activity
                {
                    State = ActivityState.Pending,
                    ClientId = "123456789",
                    Periodicity = PeriodicityType.Casual
                };

                _activitiesRepository.Insert(activity);

                await _dbContext.SaveChangesAsync();

                Assert.Fail();
            }
            catch (DbUpdateException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task Search_Existing_Activity()
        {
            Activity activity = await _activitiesRepository.FindByIdAsync(1);

            Assert.IsNotNull(activity);
            Assert.AreEqual(PeriodicityType.Casual, activity.Periodicity);
            Assert.AreEqual(ActivityState.Pending, activity.State);
        }

        [Test]
        public async Task Search_Non_Existent_Activity()
        {
            Activity activity = await _activitiesRepository.FindByIdAsync(2);

            Assert.IsNull(activity);
        }

        [Test]
        public async Task Update_Existing_Activity()
        {
            Activity activity = await _activitiesRepository.FindByIdAsync(1);
            Assert.IsNotNull(activity);

            activity.State = ActivityState.Accepted;

            _activitiesRepository.Update(activity);
            await _dbContext.SaveChangesAsync();

            activity = await _activitiesRepository.FindByIdAsync(1);
            Assert.IsNotNull(activity);
            Assert.AreEqual(ActivityState.Accepted, activity.State);
        }
    }
}
