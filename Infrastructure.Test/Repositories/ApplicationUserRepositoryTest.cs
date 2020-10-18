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
        IApplicationUserRepository applicationUserRepository;

        [SetUp]
        public void SetUp()
        {
            applicationUserRepository = ServiceProvider.GetService<IApplicationUserRepository>();
        }

        [Test]
        [Order(0)]
        public void CheckApplicationUserRepository()
        {
            Assert.IsNotNull(applicationUserRepository);
        }

        [Test]
        [Order(1)]
        public async Task SaveUser()
        {
            try
            {
                ApplicationUser applicationUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "example@example.com",
                    PhoneNumber = "3167040706"
                };

                IdentityResult result = await applicationUserRepository.CreateAsync(applicationUser, "ThisisaSecurePassword321*");

                Assert.IsTrue(result.Succeeded);
            }
            catch (DbUpdateException)
            {
                Assert.Fail();
            }
        }

        [Test]
        [Order(2)]
        public async Task SearchUserByUserName()
        {
            ApplicationUser applicationUser = await applicationUserRepository.FindByNameAsync("admin");

            Assert.IsNotNull(applicationUser);
            Assert.AreEqual("admin", applicationUser.UserName);
        }

        [Test]
        [Order(3)]
        public async Task SearchUserByEmail()
        {
            ApplicationUser applicationUser = await applicationUserRepository.FindByEmailAsync("example@example.com");

            Assert.IsNotNull(applicationUser);
            Assert.AreEqual("example@example.com", applicationUser.Email);
        }

        [Test]
        [Order(4)]
        public async Task SearchUserByUserNameOrEmail()
        {
            ApplicationUser applicationUser = await applicationUserRepository.FindByNameOrEmailAsync("example@example.com");

            Assert.IsNotNull(applicationUser);
            Assert.AreEqual("example@example.com", applicationUser.Email);
        }

        [Test]
        [Order(5)]
        public async Task AssignUserRole()
        {
            try
            {
                ApplicationUser applicationUser = await applicationUserRepository.FindByNameOrEmailAsync("admin");

                IdentityResult result = await applicationUserRepository.AddToRoleAsync(applicationUser, "Administrator");

                Assert.IsTrue(result.Succeeded);
            }
            catch (DbUpdateException)
            {
                Assert.Fail();
            }
        }

        [Test]
        [Order(6)]
        public async Task CheckUserRole()
        {
            ApplicationUser applicationUser = await applicationUserRepository.FindByNameOrEmailAsync("admin");

            string role = await applicationUserRepository.GetUserRoleAsync(applicationUser);
            Assert.IsNotNull(role);
            Assert.AreEqual("Administrator", role);
        }

        [Test]
        [Order(7)]
        public async Task Generate_ConfirmEmailToken_And_ConfirmEmail()
        {
            ApplicationUser applicationUser = await applicationUserRepository.FindByNameOrEmailAsync("admin");
            Assert.IsNotNull(applicationUser);

            string confirmationToken = await applicationUserRepository.GenerateEmailConfirmationTokenAsync(applicationUser);
            Assert.IsNotNull(confirmationToken);

            ApplicationUser result = await applicationUserRepository.ConfirmEmailAsync(applicationUser, confirmationToken);

            Assert.IsNotNull(result);
            Assert.IsTrue(applicationUser.EmailConfirmed);
        }

        [Test]
        [Order(8)]
        public async Task ChangePassowrd_Without_ResetToken()
        {
            ApplicationUser applicationUser = await applicationUserRepository.FindByNameOrEmailAsync("admin");
            Assert.IsNotNull(applicationUser);

            IdentityResult result = await applicationUserRepository.ChangePassswordAsync(applicationUser, "ThisisaSecurePassword321*", "ThisIsMyNewPassword123.");

            Assert.IsTrue(result.Succeeded);
        }

        [Test]
        [Order(9)]
        public async Task ChangePassowrd_With_ResetToken()
        {
            ApplicationUser applicationUser = await applicationUserRepository.FindByNameOrEmailAsync("admin");
            Assert.IsNotNull(applicationUser);

            string passwordResetToken = await applicationUserRepository.GeneratePasswordResetTokenAsync(applicationUser);
            Assert.IsNotNull(passwordResetToken);

            IdentityResult result = await applicationUserRepository.ResetPasswordAsync(applicationUser, passwordResetToken, "ThisIsMyResetPassword321*");

            Assert.IsTrue(result.Succeeded);
        }
    }
}
