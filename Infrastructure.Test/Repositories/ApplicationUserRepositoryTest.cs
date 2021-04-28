using System.Threading.Tasks;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Infrastructure.Test.Repositories
{
    [TestFixture]
    public class ApplicationUserRepositoryTest : BaseRepositoryTest
    {
        public static string SavedUserId { get; private set; }

        private IApplicationUserRepository _applicationUserRepository;

        [SetUp]
        public void SetUp()
        {
            _applicationUserRepository = ServiceProvider.GetService<IApplicationUserRepository>();
        }

        [Test]
        [Order(0)]
        public void CheckApplicationUserRepository()
        {
            Assert.IsNotNull(_applicationUserRepository);
        }

        [Test]
        [Order(1)]
        public async Task Save_Valid_User()
        {
            try
            {
                ApplicationUser applicationUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "example@example.com",
                    PhoneNumber = "3167040706"
                };

                IdentityResult result =
                    await _applicationUserRepository.CreateAsync(applicationUser, "ThisisaSecurePassword321*");

                Assert.IsTrue(result.Succeeded);

                SavedUserId = applicationUser.Id;
            }
            catch (DbUpdateException e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public async Task Save_Invalid_User()
        {
            try
            {
                ApplicationUser applicationUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "example@example.com",
                    PhoneNumber = "31670407"
                };

                IdentityResult result =
                    await _applicationUserRepository.CreateAsync(applicationUser, "notsecurepassword");

                Assert.IsTrue(!result.Succeeded);
            }
            catch (DbUpdateException)
            {
                Assert.Pass();
            }
        }

        [Test]
        [Order(2)]
        public async Task Search_User_By_UserName()
        {
            ApplicationUser applicationUser = await _applicationUserRepository.FindByNameAsync("admin");

            Assert.IsNotNull(applicationUser);
            Assert.AreEqual("admin", applicationUser.UserName);
        }

        [Test]
        [Order(3)]
        public async Task Search_User_By_Email()
        {
            ApplicationUser applicationUser = await _applicationUserRepository.FindByEmailAsync("example@example.com");

            Assert.IsNotNull(applicationUser);
            Assert.AreEqual("example@example.com", applicationUser.Email);
        }

        [Test]
        [Order(4)]
        public async Task Search_User_By_UserName_Or_Email()
        {
            ApplicationUser applicationUser =
                await _applicationUserRepository.FindByNameOrEmailAsync("example@example.com");

            Assert.IsNotNull(applicationUser);
            Assert.AreEqual("example@example.com", applicationUser.Email);
        }

        [Test]
        [Order(5)]
        public async Task Assign_Roles_To_User()
        {
            try
            {
                ApplicationUser applicationUser = await _applicationUserRepository.FindByNameOrEmailAsync("admin");

                IdentityResult administratorRole =
                    await _applicationUserRepository.AddToRoleAsync(applicationUser, "Administrator");
                IdentityResult clientRole = await _applicationUserRepository.AddToRoleAsync(applicationUser, "Client");
                IdentityResult officeEmployeeRole =
                    await _applicationUserRepository.AddToRoleAsync(applicationUser, "OfficeEmployee");
                IdentityResult technicalEmployeeRole =
                    await _applicationUserRepository.AddToRoleAsync(applicationUser, "TechnicalEmployee");

                Assert.IsTrue(administratorRole.Succeeded);
                Assert.IsTrue(clientRole.Succeeded);
                Assert.IsTrue(officeEmployeeRole.Succeeded);
                Assert.IsTrue(technicalEmployeeRole.Succeeded);
            }
            catch (DbUpdateException e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        [Order(7)]
        public async Task Generate_ConfirmEmailToken_And_ConfirmEmail()
        {
            ApplicationUser applicationUser = await _applicationUserRepository.FindByNameOrEmailAsync("admin");
            Assert.IsNotNull(applicationUser);

            string confirmationToken =
                await _applicationUserRepository.GenerateEmailConfirmationTokenAsync(applicationUser);
            Assert.IsNotNull(confirmationToken);

            ApplicationUser result =
                await _applicationUserRepository.ConfirmEmailAsync(applicationUser, confirmationToken);

            Assert.IsNotNull(result);
            Assert.IsTrue(applicationUser.EmailConfirmed);
        }

        [Test]
        [Order(8)]
        public async Task ChangePassword_Without_ResetToken()
        {
            ApplicationUser applicationUser = await _applicationUserRepository.FindByNameOrEmailAsync("admin");
            Assert.IsNotNull(applicationUser);

            IdentityResult result = await _applicationUserRepository.ChangePasswordAsync(applicationUser,
                "ThisisaSecurePassword321*", "ThisIsMyNewPassword123.");

            Assert.IsTrue(result.Succeeded);
        }

        [Test]
        [Order(9)]
        public async Task ChangePassword_With_ResetToken()
        {
            ApplicationUser applicationUser = await _applicationUserRepository.FindByNameOrEmailAsync("admin");
            Assert.IsNotNull(applicationUser);

            string passwordResetToken =
                await _applicationUserRepository.GeneratePasswordResetTokenAsync(applicationUser);
            Assert.IsNotNull(passwordResetToken);

            IdentityResult result = await _applicationUserRepository.ResetPasswordAsync(applicationUser,
                passwordResetToken, "ThisIsMyResetPassword321*");

            Assert.IsTrue(result.Succeeded);
        }
    }
}
