using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.Notification;
using Kaizen.Test.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Kaizen.Test.Controllers
{
    [TestFixture]
    public class NotificationsControllerTest : BaseControllerTest
    {
        private NotificationsController _notificationsController;
        private Mock<INotificationsRepository> _notificationsRepository;
        private Mock<IUnitWork> _unitWork;

        [SetUp]
        public void SetUp()
        {
            _notificationsRepository = new Mock<INotificationsRepository>();
            _unitWork = new Mock<IUnitWork>();

            _notificationsController = new NotificationsController(_notificationsRepository.Object, _unitWork.Object,
                ServiceProvider.GetService<IMapper>());

            SetUpNotificationsRepository();
            SetUpUnitWork();
        }

        private void SetUpNotificationsRepository()
        {
            _notificationsRepository.Setup(r => r.Where(It.IsAny<Expression<Func<Notification, bool>>>()))
                .Returns((Expression<Func<Notification, bool>> predicate) =>
                {
                    var notifications = new TestAsyncEnumerable<Notification>(new[]
                    {
                        new Notification
                        {
                            Id = 1,
                            Icon = "face",
                            Message = "El cliente manolo se ha registrado.",
                            State = NotificationState.Pending,
                            Title = "Nuevo cliente",
                            UserId = "333-555-333"
                        },
                        new Notification
                        {
                            Id = 1,
                            Icon = "payments",
                            Message = "El cliente manolo ha realizado el pego de su factura.",
                            State = NotificationState.Pending,
                            Title = "Pago de factura",
                            UserId = "333-555-332"
                        }
                    });

                    return notifications.Where(predicate);
                });

            _notificationsRepository.Setup(r => r.FindByIdAsync(1)).ReturnsAsync(new Notification
            {
                Id = 1,
                Icon = "face",
                Message = "El cliente Manolo se ha registrado",
                Title = "Registro de cliente",
                State = NotificationState.Pending,
                UserId = "333-555-555"
            });
            _notificationsRepository.Setup(r => r.FindByIdAsync(3)).ReturnsAsync((Notification) null);

            _notificationsRepository.Setup(r => r.Update(It.IsAny<Notification>())).Verifiable();
        }

        private void SetUpUnitWork()
        {
            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);
        }

        [Test]
        public async Task Get_All_Notification_Of_Existing_User()
        {
            var result = (await _notificationsController.GetNotifications("333-555-333")).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);

            Assert.IsInstanceOf<IEnumerable<NotificationViewModel>>(result.Value);

            var notifications = result.Value as IEnumerable<NotificationViewModel>;
            Assert.IsNotNull(notifications);
            Assert.AreEqual(1, notifications.Count());
        }

        [Test]
        public async Task Get_All_Notifications_Of_Non_Existent_User()
        {
            var result = (await _notificationsController.GetNotifications("333-555-331")).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);

            Assert.IsInstanceOf<IEnumerable<NotificationViewModel>>(result.Value);

            var notifications = result.Value as IEnumerable<NotificationViewModel>;
            Assert.IsNotNull(notifications);
            Assert.AreEqual(0, notifications.Count());
        }

        [Test]
        public async Task Update_Existing_Notification()
        {
            var result = await _notificationsController.PutNotification(1, new NotificationEditModel
            {
                State = NotificationState.View
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(NotificationState.View, result.Value.State);
        }

        [Test]
        public async Task Update_Non_Existent_Notification()
        {
            var result = await _notificationsController.PutNotification(3, new NotificationEditModel
            {
                State = NotificationState.View
            });

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
    }
}
