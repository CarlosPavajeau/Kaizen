using System.Threading.Tasks;
using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Kaizen.Domain.Repositories
{
    public interface IApplicationUserRepository : IRepositoryBase<ApplicationUser, string>
    {
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<SignInResult> Login(ApplicationUser user, string password);
        Task<ApplicationUser> FindByNameAsync(string username);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<ApplicationUser> FindByNameOrEmailAsync(string usernameOrEmail);

        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);
        Task<string> GetUserRoleAsync(ApplicationUser user);

        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);

        Task<ApplicationUser> ConfirmEmailAsync(ApplicationUser user, string token);
    }
}
