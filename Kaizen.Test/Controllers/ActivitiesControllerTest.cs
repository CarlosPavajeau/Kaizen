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

            SetUpActivitiesRepository();
            SetUpClientsRepository();
            SetUpUnitWork();
        }

        private void SetUpActivitiesRepository()
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

            _activitiesRepository.Setup(r => r.FindByIdAsync(1)).ReturnsAsync(new Activity
            {
                Code = 1,
                ClientId = "1007870922",
                State = ActivityState.Pending
            });
            _activitiesRepository.Setup(r => r.FindByIdAsync(3)).ReturnsAsync((Activity)null);

            _activitiesRepository.Setup(r => r.GetPendingEmployeeActivities("32752423", It.IsAny<DateTime>()))
                .ReturnsAsync(new[]
                {
                    new Activity
                    {
                        Code = 1,
                        ClientId = "1007870922"
                    }
                });

            _activitiesRepository.Setup(r => r.GetPendingClientActivities("1007870922")).ReturnsAsync(new List<Activity>
            {
                new()
                {
                    Code = 1,
                    ClientId = "1007870922"
                }
            });

            _activitiesRepository.Setup(r => r.GetAppliedClientActivities("1007870922")).ReturnsAsync(new List<Activity>
            {
                new()
                {
                    Code = 1,
                    ClientId = "1007870923"
                }
            });

            _activitiesRepository.Setup(r => r.Insert(It.IsAny<Activity>())).Verifiable();
            _activitiesRepository.Setup(r => r.Update(It.IsAny<Activity>())).Verifiable();
        }

        private void SetUpClientsRepository()
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

            _clientsRepository.Setup(r => r.GetClientWithUser("1007870930")).ReturnsAsync((Client)null);
        }

        private void SetUpUnitWork()
        {
            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);
        }

        [Test]
        public async Task Get_All_Activities()
        {
            OkObjectResult result = (await _activitiesController.GetActivities()).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);

            Assert.IsInstanceOf<IEnumerable<ActivityViewModel>>(result.Value);

            IEnumerable<ActivityViewModel> value = result.Value as IEnumerable<ActivityViewModel>;
            Assert.IsNotNull(value);
            Assert.AreEqual(2, value.Count());
        }

        [Test]
        public async Task Get_Existing_Activity()
        {
            ActionResult<ActivityViewModel> result = await _activitiesController.GetActivity(1);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(1, result.Value.Code);
        }

        [Test]
        public async Task Get_Non_Existent_Activity()
        {
            ActionResult<ActivityViewModel> result = await _activitiesController.GetActivity(3);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task Get_Employee_Activities()
        {
            OkObjectResult result = (await _activitiesController.EmployeeActivities("32752423", DateTime.Now)).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task Get_Client_Activities()
        {
            OkObjectResult result = (await _activitiesController.ClientActivities("1007870922")).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task Get_Applied_Client_Activities()
        {
            OkObjectResult result = (await _activitiesController.AppliedClientActivities("1007870922")).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task Update_Existing_Activity()
        {
            ActionResult<ActivityViewModel> result = await _activitiesController.PutActivity(1, new ActivityEditModel
            {
                State = ActivityState.Accepted,
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(ActivityState.Accepted, result.Value.State);
        }

        [Test]
        public async Task Update_Non_Existent_Activity()
        {
            var result = await _activitiesController.PutActivity(3, new ActivityEditModel
            {
                State = ActivityState.Accepted,
            });

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Save_Activity_With_Existing_Client()
        {
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

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task Save_Activity_With_Non_Existing_Client()
        {
            var result = await _activitiesController.PostActivity(new ActivityInputModel
            {
                ClientId = "1007870930",
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

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task DeleteActivity()
        {
            _activitiesRepository.Setup(r => r.Delete(It.IsAny<Activity>())).Verifiable();
            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            ActionResult<ActivityViewModel> result = await _activitiesController.DeleteActivity(1);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }
    }
}
