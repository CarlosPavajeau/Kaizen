using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers;
using Kaizen.Core.Security;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Extensions;
using Kaizen.Models.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        [SetUp]
        public void Setup()
        {
            _applicationUserRepository = new Mock<IApplicationUserRepository>();
            _usersController = new UsersController(_applicationUserRepository.Object,
                ServiceProvider.GetService<IMapper>(), ServiceProvider.GetService<ITokenGenerator>());
        }

        [Test]
        public async Task GetUser()
        {
            _applicationUserRepository.Setup(r => r.FindByIdAsync("333-444-555")).Returns(Task.FromResult(
                new ApplicationUser
                {
                    Id = "333-444-555",
                    UserName = "admin"
                }));

            ActionResult<ApplicationUserViewModel> result = await _usersController.GetUser("333-444-555");
            ApplicationUserViewModel userView = result.Value;

            Assert.NotNull(result);
            Assert.NotNull(userView);
            Assert.AreEqual("333-444-555", userView.Id);
        }

        [Test]
        public async Task ChangePassword()
        {
            _applicationUserRepository.Setup(r => r.FindByIdAsync("333-444-555")).Returns(Task.FromResult(
                new ApplicationUser
                {
                    Id = "333-444-555",
                    UserName = "admin"
                }));

            _applicationUserRepository.Setup(r =>
                r.ChangePasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));

            ActionResult<ApplicationUserViewModel> result = await _usersController.ChangePassword("333-444-555",
                new ChangePasswordModel { NewPassword = "newPassword", OldPassword = "oldPassword" });

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }

        [Test]
        public async Task ResetPassword()
        {
            _applicationUserRepository.Setup(r => r.FindByNameOrEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(
                new ApplicationUser
                {
                    Id = "333-444-555",
                    UserName = "admin"
                }));

            _applicationUserRepository.Setup(r =>
                r.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));

            ActionResult<ApplicationUserViewModel> result = await _usersController.ResetPassword("admin", new ResetPasswordModel
            {
                Token = "ResetPasswordToken".Base64ForUrlEncode(),
                NewPassword = "ThisIsMyNewPassword"
            });

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }


        [Test]
        public async Task Login()
        {
            _applicationUserRepository.Setup(r => r.FindByNameOrEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(
                new ApplicationUser
                {
                    Id = "333-444-555",
                    UserName = "admin"
                }));

            _applicationUserRepository.Setup(r => r.GetUserRoleAsync(It.IsAny<ApplicationUser>()))
                .Returns(Task.FromResult("Administrator"));

            _applicationUserRepository.Setup(r => r.Login(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(Microsoft.AspNetCore.Identity.SignInResult.Success));

            ActionResult<ApplicationUserViewModel> result = await _usersController.Login(new LoginRequest
            {
                UsernameOrEmail = "Admin",
                Password = "ThisIsAPassword"
            });

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }

        [Test]
        public async Task ConfirmEmail()
        {
            _applicationUserRepository.Setup(r => r.FindByNameOrEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(
                new ApplicationUser
                {
                    Id = "333-444-555",
                    UserName = "admin",
                    EmailConfirmed = false
                }));

            _applicationUserRepository.Setup(r => r.ConfirmEmailAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new ApplicationUser
                {
                    Id = "333-444-555",
                    UserName = "admin",
                    EmailConfirmed = true
                }));

            ActionResult<ApplicationUserViewModel> result =
                await _usersController.ConfirmEmail("ConfirmEmailToken".Base64ForUrlEncode(), "admin@admin.com");

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }
    }
}
