using System.Threading.Tasks;
using Kaizen.Core.Exceptions;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Microsoft.AspNetCore.Identity;

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
            IdentityResult result = await _userManager.CreateAsync(entity);
            if (!result.Succeeded)
                throw new UserNotCreate();
        }

        public async override void Update(ApplicationUser entity)
        {
            IdentityResult result = await _userManager.UpdateAsync(entity);
            if (!result.Succeeded)
                throw new UserNotUpdate();
        }

        public async Task<SignInResult> Login(ApplicationUser user, string password)
        {
            return await _signInManager.PasswordSignInAsync(user, password, false, false);
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
                user = await FindByEmailAsync(usernameOrEmail);

            return user;
        }
    }
}
