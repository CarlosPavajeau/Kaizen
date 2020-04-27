using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Kaizen.Domain.Repositories
{
    public interface IApplicationUserRepository : IRepositoryBase<ApplicationUser, string>
    {
        Task<SignInResult> Login(ApplicationUser user, string password);
    }
}
