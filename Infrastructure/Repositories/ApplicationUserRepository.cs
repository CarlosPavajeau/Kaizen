using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Core.Services;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Kaizen.Infrastructure.Repositories
{
    public class ApplicationUserRepository : RepositoryBase<ApplicationUser, string>, IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMailTemplate _mailTemplate;
        private readonly IMailService _mailService;

        public ApplicationUserRepository(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMailTemplate mailTemplate,
            IMailService mailService
            ) : base(dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailTemplate = mailTemplate;
            _mailService = mailService;
        }

        public async Task<SignInResult> Login(ApplicationUser user, string password, bool isPersistent)
        {
            return await _signInManager.PasswordSignInAsync(user, password, isPersistent, false);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<ApplicationUser> FindByNameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<ApplicationUser> FindByNameOrEmailAsync(string usernameOrEmail)
        {
            ApplicationUser user = await FindByNameAsync(usernameOrEmail);

            if (user is null)
            {
                user = await FindByEmailAsync(usernameOrEmail);
            }

            return user;
        }

        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<string> GetUserRoleAsync(ApplicationUser user)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<ApplicationUser> ConfirmEmailAsync(ApplicationUser user, string token)
        {
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);
            return !result.Succeeded ? null : user;
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task<bool> SendPasswordResetTokenAsync(ApplicationUser user, string resetPasswordLink)
        {
            if (user is null)
            {
                return false;
            }

            string mailTemplate = _mailTemplate.LoadTemplate("ResetPassword.html", resetPasswordLink);

            await _mailService.SendEmailAsync(user.Email, "Contraseña olvidada", mailTemplate, true);

            return true;
        }
    }
}
