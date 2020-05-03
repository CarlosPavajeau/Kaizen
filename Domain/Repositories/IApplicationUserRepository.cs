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
    }
}
