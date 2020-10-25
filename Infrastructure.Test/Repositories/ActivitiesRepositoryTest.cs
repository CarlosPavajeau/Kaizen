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
        IActivitiesRepository activitiesRepository;
        ApplicationDbContext dbContext;

        [SetUp]
        public void SetUp()
        {
            activitiesRepository = ServiceProvider.GetService<IActivitiesRepository>();
            dbContext = ServiceProvider.GetService<ApplicationDbContext>();
        }

        [Test]
        public void CheckActivitiesRepository()
        {
            Assert.IsNotNull(activitiesRepository);
        }

        [Test]
        public async Task SaveActivity()
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

                activitiesRepository.Insert(activity);

                await dbContext.SaveChangesAsync();

                Assert.Pass();
            }
            catch (DbUpdateException)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task SearchActivity()
        {
            Activity activity = await activitiesRepository.FindByIdAsync(1);

            Assert.IsNotNull(activity);
            Assert.AreEqual(PeriodicityType.Casual, activity.Periodicity);
            Assert.AreEqual(ActivityState.Pending, activity.State);
        }

        [Test]
        public async Task UpdateActivity()
        {
            Activity activity = await activitiesRepository.FindByIdAsync(1);
            Assert.IsNotNull(activity);

            activity.State = ActivityState.Accepted;

            activitiesRepository.Update(activity);
            await dbContext.SaveChangesAsync();

            activity = await activitiesRepository.FindByIdAsync(1);

            Assert.AreEqual(ActivityState.Accepted, activity.State);
        }
    }
}
