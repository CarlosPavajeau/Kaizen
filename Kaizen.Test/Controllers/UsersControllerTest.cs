using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers;
using Kaizen.Core.Security;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Extensions;
using Kaizen.Infrastructure.Identity;
using Kaizen.Middleware;
using Kaizen.Models.ApplicationUser;
using Kaizen.Test.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Kaizen.Test.Controllers
{
    [TestFixture]
    public class UsersControllerTest : BaseControllerTest
    {
        private UsersController _usersController;
        private Mock<IApplicationUserRepository> _applicationUserRepository;
        private readonly SpanishIdentityErrorDescriber _spanishIdentityErrorDescriber = new();

        [SetUp]
        public void Setup()
        {
            _applicationUserRepository = new Mock<IApplicationUserRepository>();
            _usersController = new UsersController(_applicationUserRepository.Object,
                ServiceProvider.GetService<IMapper>(), ServiceProvider.GetService<ITokenGenerator>());

            SetUpApplicationUserRepository();
        }

        private void SetUpApplicationUserRepository()
        {
            _applicationUserRepository.Setup(r => r.FindByIdAsync("333-444-555")).ReturnsAsync(
                new ApplicationUser
                {
                    Id = "333-444-555",
                    UserName = "admin",
                    Email = "admin@admin.com"
                });

            _applicationUserRepository.Setup(r => r.FindByIdAsync("123-456-789")).ReturnsAsync((ApplicationUser) null);

            _applicationUserRepository.Setup(r => r.FindByNameOrEmailAsync("admin")).ReturnsAsync(
                new ApplicationUser
                {
                    Id = "333-444-555",
                    UserName = "admin",
                    Email = "admin@admin.com"
                });

            _applicationUserRepository.Setup(r => r.FindByNameOrEmailAsync("roots2"))
                .ReturnsAsync((ApplicationUser) null);

            _applicationUserRepository.Setup(r => r.FindByNameOrEmailAsync("admin@admin.com")).ReturnsAsync(
                new ApplicationUser
                {
                    Id = "333-444-555",
                    UserName = "admin",
                    Email = "admin@admin.com"
                });

            _applicationUserRepository.Setup(r => r.FindByNameOrEmailAsync("roots2@roots2.com"))
                .ReturnsAsync((ApplicationUser) null);

            _applicationUserRepository.Setup(r => r.GetAll()).Returns(new TestAsyncEnumerable<ApplicationUser>(new[]
            {
                new ApplicationUser
                {
                    Id = "333-444-555",
                    UserName = "admin"
                }
            }).AsQueryable());

            _applicationUserRepository.Setup(r =>
                    r.ChangePasswordAsync(It.IsAny<ApplicationUser>(), "WrongOldPassword", It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Failed(_spanishIdentityErrorDescriber.PasswordMismatch())));

            _applicationUserRepository.Setup(r =>
                    r.ChangePasswordAsync(It.IsAny<ApplicationUser>(), "CorrectOldPassword", It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            _applicationUserRepository.Setup(r =>
                    r.ResetPasswordAsync(It.IsAny<ApplicationUser>(), "WrongToken", It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Failed(_spanishIdentityErrorDescriber.InvalidToken())));

            _applicationUserRepository.Setup(r =>
                    r.ResetPasswordAsync(It.IsAny<ApplicationUser>(), "ValidToken", It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            _applicationUserRepository.Setup(r => r.GetUserRoleAsync(It.IsAny<ApplicationUser>()))
                .Returns(Task.FromResult("Administrator"));

            _applicationUserRepository
                .Setup(r => r.Login(It.IsAny<ApplicationUser>(), "WrongPassword", It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            _applicationUserRepository
                .Setup(r => r.Login(It.IsAny<ApplicationUser>(), "CorrectPassword", It.IsAny<bool>()))
                .Returns(Task.FromResult(Microsoft.AspNetCore.Identity.SignInResult.Success));

            _applicationUserRepository.Setup(r => r.ConfirmEmailAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new ApplicationUser
                {
                    Id = "333-444-555",
                    UserName = "admin",
                    EmailConfirmed = true
                }));

            _applicationUserRepository.Setup(r => r.Logout()).Returns(Task.CompletedTask);

            _applicationUserRepository.Setup(r => r.GeneratePasswordResetTokenAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync("PasswordResetToken");

            _applicationUserRepository
                .Setup(r => r.SendPasswordResetTokenAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(true);
        }

        [Test]
        public async Task Get_Existing_User()
        {
            ActionResult<ApplicationUserViewModel> result = await _usersController.GetUser("333-444-555");
            ApplicationUserViewModel userView = result.Value;

            Assert.NotNull(result);
            Assert.NotNull(userView);
            Assert.AreEqual("333-444-555", userView.Id);
        }

        [Test]
        public async Task Get_Non_Existent_User()
        {
            var result = await _usersController.GetUser("123-456-789");

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task Check_User_Exists()
        {
            var result = await _usersController.CheckExists("admin");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value);
        }

        [Test]
        public async Task Change_Password_Of_Non_Existent_User()
        {
            var result = await _usersController.ChangePassword("123-456-789",
                new ChangePasswordModel {NewPassword = "newPassword", OldPassword = "oldPassword"});

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Change_Password_Of_An_Existing_User_With_Wrong_Old_Password()
        {
            var result = await _usersController.ChangePassword("333-444-555",
                new ChangePasswordModel {NewPassword = "NewPassword", OldPassword = "WrongOldPassword"});

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Change_Password_Of_An_Existing_User_With_Correct_Old_Password()
        {
            var result = await _usersController.ChangePassword("333-444-555",
                new ChangePasswordModel {NewPassword = "NewPassword", OldPassword = "CorrectOldPassword"});

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task Reset_Password_Of_Non_Existent_User()
        {
            ActionResult<ApplicationUserViewModel> result = await _usersController.ResetPassword("roots2",
                new ResetPasswordModel
                {
                    Token = "ResetPasswordToken".Base64ForUrlEncode(),
                    NewPassword = "ThisIsMyNewPassword"
                });

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Reset_Password_Of_An_Existing_User_With_Wrong_Token()
        {
            var result = await _usersController.ResetPassword("admin", new ResetPasswordModel
            {
                Token = "WrongToken".Base64ForUrlEncode(),
                NewPassword = "NewPassword"
            });

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Reset_Password_Of_An_Existing_User_With_Valid_Token()
        {
            var result = await _usersController.ResetPassword("admin", new ResetPasswordModel
            {
                Token = "ValidToken".Base64ForUrlEncode(),
                NewPassword = "NewPassword"
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task Login_Of_Non_Existent_User()
        {
            var result = await _usersController.Login(new LoginRequest
            {
                UsernameOrEmail = "roots2",
                Password = "ThisIsAPassword"
            });

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
        }

        [Test]
        public async Task Login_Of_Non_Existing_User_With_Wrong_Password()
        {
            var result = await _usersController.Login(new LoginRequest
            {
                UsernameOrEmail = "admin",
                Password = "WrongPassword"
            });

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Login_Of_Non_Existing_User_With_Correct_Password()
        {
            var result = await _usersController.Login(new LoginRequest
            {
                UsernameOrEmail = "admin",
                Password = "CorrectPassword"
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task Logout()
        {
            var result = await _usersController.Logout();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value);
        }

        [Test]
        public async Task Confirm_Email_Of_Non_Existent_User()
        {
            var result =
                await _usersController.ConfirmEmail("ConfirmEmailToken".Base64ForUrlEncode(), "roots2@roots2.com");

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task Confirm_Email_Of_An_Existing_User()
        {
            var result =
                await _usersController.ConfirmEmail("ConfirmEmailToken".Base64ForUrlEncode(), "admin@admin.com");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task Forgotten_Password_Of_Non_Existent_User()
        {
            var result = await _usersController.ForgottenPassword("roots2");

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Value);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Forgotten_Password_Of_An_Existing_User()
        {
            MockHttpContext();

            var result = await _usersController.ForgottenPassword("admin");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value);
        }

        private void MockHttpContext()
        {
            var urlHelper = new Mock<IUrlHelper>();
            var httpContext = new Mock<HttpContext>();
            var httpRequest = new Mock<HttpRequest>();

            urlHelper.Setup(r => r.Action(It.IsAny<UrlActionContext>())).Returns("callbackUrl").Verifiable();

            httpRequest.SetupGet(r => r.Scheme).Returns("https");
            httpRequest.SetupGet(r => r.Host).Returns(new HostString("localhost"));
            httpRequest.SetupGet(r => r.PathBase).Returns(PathString.Empty);

            httpContext.SetupGet(r => r.Request).Returns(httpRequest.Object);

            _usersController.Url = urlHelper.Object;
            _usersController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext.Object
            };

            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.SetupGet(r => r.HttpContext).Returns(httpContext.Object);

            KaizenHttpContext.Configure(httpContextAccessor.Object);
        }
    }
}
