using Kaizen.Core.Exceptions;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Kaizen.Domain.Repositories
{
    public class ApplicationUserRepository : RepositoryBase<ApplicationUser, string>, IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ApplicationUserRepository(
            ApplicationDbContext dbContext, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            ) : base(dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async override void Insert(ApplicationUser entity)
        {
            var result = await _userManager.CreateAsync(entity);
            if (!result.Succeeded)
                throw new UserNotCreate();
        }

        public async Task<SignInResult> Login(ApplicationUser user, string password)
        {
            return await _signInManager.PasswordSignInAsync(user, password, false, false);
        }
    }
}
