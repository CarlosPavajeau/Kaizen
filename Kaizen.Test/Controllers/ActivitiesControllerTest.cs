using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.Activity;
using Kaizen.Test.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Kaizen.Test.Controllers
{
    [TestFixture]
    public class ActivitiesControllerTest : BaseControllerTest
    {
        private ActivitiesController _activitiesController;
        private Mock<IActivitiesRepository> _activitiesRepository;
        private Mock<IClientsRepository> _clientsRepository;
        private Mock<IUnitWork> _unitWork;

        [SetUp]
        public void SetUp()
        {
            _activitiesRepository = new Mock<IActivitiesRepository>();
            _clientsRepository = new Mock<IClientsRepository>();
            _unitWork = new Mock<IUnitWork>();

            _activitiesController = new ActivitiesController(_activitiesRepository.Object, _clientsRepository.Object,
                _unitWork.Object, ServiceProvider.GetService<IMapper>());
        }

        [Test]
        public async Task GetActivities()
        {
            _activitiesRepository.Setup(r => r.GetAll()).Returns(new TestAsyncEnumerable<Activity>(new List<Activity>
            {
                new()
                {
                    Code = 1,
                    ClientId = "1007870923",
                    Client = new Client
                    {
                        Id = "1007870923",
                        FirstName = "Manolo"
                    }
                },
                new()
                {
                    Code = 2,
                    ClientId = "1007870922",
                    Client = new Client
                    {
                        Id = "1007870922",
                        FirstName = "Pedro"
                    }
                }
            }).AsQueryable());

            OkObjectResult result = (await _activitiesController.GetActivities()).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Value);

            Assert.IsInstanceOf<IEnumerable<ActivityViewModel>>(result.Value);

            IEnumerable<ActivityViewModel> value = result.Value as IEnumerable<ActivityViewModel>;
            Assert.NotNull(value);
            Assert.AreEqual(2, value.Count());
        }

        [Test]
        public async Task GetActivity()
        {
            _activitiesRepository.Setup(r => r.FindByIdAsync(1)).ReturnsAsync(new Activity
            {
                Code = 1,
                ClientId = "1007870922"
            });

            ActionResult<ActivityViewModel> result = await _activitiesController.GetActivity(1);

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.AreEqual(1, result.Value.Code);
        }

        [Test]
        public async Task EmployeeActivities()
        {
            _activitiesRepository.Setup(r => r.GetPendingEmployeeActivities("32752423", It.IsAny<DateTime>()))
                .ReturnsAsync(new[]
                {
                    new Activity
                    {
                        Code = 1,
                        ClientId = "1007870922"
                    }
                });

            OkObjectResult result = (await _activitiesController.EmployeeActivities("32752423", DateTime.Now)).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }

        [Test]
        public async Task ClientActivities()
        {
            _activitiesRepository.Setup(r => r.GetPendingClientActivities("1007870922")).ReturnsAsync(new List<Activity>
            {
                new()
                {
                    Code = 1,
                    ClientId = "1007870922"
                }
            });

            OkObjectResult result = (await _activitiesController.ClientActivities("1007870922")).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }

        [Test]
        public async Task AppliedClientActivities()
        {
            _activitiesRepository.Setup(r => r.GetAppliedClientActivities("1007870922")).ReturnsAsync(new List<Activity>
            {
                new()
                {
                    Code = 1,
                    ClientId = "1007870923"
                }
            });

            OkObjectResult result = (await _activitiesController.AppliedClientActivities("1007870922")).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }

        [Test]
        public async Task PutActivity()
        {
            _activitiesRepository.Setup(r => r.FindByIdAsync(1)).ReturnsAsync(new Activity
            {
                Code = 1,
                ClientId = "1007870922",
                State = ActivityState.Pending
            });
            _activitiesRepository.Setup(r => r.Update(It.IsAny<Activity>())).Verifiable();

            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            ActionResult<ActivityViewModel> result = await _activitiesController.PutActivity(1, new ActivityEditModel
            {
                State = ActivityState.Accepted,
            });

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.AreEqual(ActivityState.Accepted, result.Value.State);
        }

        [Test]
        public async Task PostActivity()
        {
            _clientsRepository.Setup(r => r.GetClientWithUser("1007870922")).ReturnsAsync(new Client
            {
                Id = "1007870922",
                FirstName = "Manolo",
                User = new ApplicationUser
                {
                    Id = "111-222",
                    UserName = "client"
                }
            });
            _activitiesRepository.Setup(r => r.Insert(It.IsAny<Activity>())).Verifiable();
            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            ActionResult<ActivityViewModel> result = await _activitiesController.PostActivity(new ActivityInputModel
            {
                ClientId = "1007870922",
                Date = DateTime.Now,
                ServiceCodes = new List<string>
                {
                    "123",
                    "321"
                },
                EmployeeCodes = new List<string>
                {
                    "1007870945"
                }
            });

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }

        [Test]
        public async Task DeleteActivity()
        {
            _activitiesRepository.Setup(r => r.FindByIdAsync(1)).ReturnsAsync(new Activity
            {
                Code = 1,
                ClientId = "1007870922"
            });
            _activitiesRepository.Setup(r => r.Delete(It.IsAny<Activity>())).Verifiable();
            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            var result = await _activitiesController.DeleteActivity(1);

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }
    }
}
