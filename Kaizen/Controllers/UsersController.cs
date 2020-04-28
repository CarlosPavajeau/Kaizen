using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.EditModels;
using Kaizen.Infrastructure.Security;
using Kaizen.InputModels;
using Kaizen.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Kaizen.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IApplicationUserRepository UserRepository { get; }

        public UsersController(
            IApplicationUserRepository userRepository,
            IConfiguration configuration
            )
        {
            UserRepository = userRepository;
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserViewModel>> GetUser(string id)
        {
            ApplicationUser user = await UserRepository.FindByIdAsync(id);

            if (user == null)
                return NotFound();
            return new ApplicationUserViewModel(user);
        }

        [HttpGet("[action]/{username}")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> CheckUserExists(string username)
        {
            return (await UserRepository.FindByNameAsync(username)) != null;
        }

        // PUT: api/Users/{id}?token={token}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, [FromQuery] string token, [FromBody] ApplicationUserEditModel editModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserRepository.FindByIdAsync(id);

                if (user != null)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // POST: api/Users
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ApplicationUserViewModel>> PostUser([FromBody]ApplicationUserInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = inputModel.Username,
                    Email = inputModel.Email,
                    PhoneNumber = inputModel.PhoneNumber
                };

                IdentityResult result = await UserRepository.CreateAsync(user, inputModel.Password);
                if (result.Succeeded)
                    return GenerateAuthenticateUser(user);
                else
                    return BadRequest();
            }
            else
                return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult<ApplicationUserViewModel>> Login([FromBody] LoginRequest login)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserRepository.FindByNameAsync(login.UsernameOrEmail);
                if (user is null)
                {
                    user = await UserRepository.FindByEmailAsync(login.UsernameOrEmail);
                    if (user is null)
                        return NotFound();
                }

                Microsoft.AspNetCore.Identity.SignInResult result = await UserRepository.Login(user, login.Password);
                if (result.Succeeded)
                    return GenerateAuthenticateUser(user);
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        private ActionResult<ApplicationUserViewModel> GenerateAuthenticateUser(ApplicationUser user)
        {
            ApplicationUserViewModel userView = new ApplicationUserViewModel(user);

            userView.Token = JwtSecurityTokenGenerator.GenerateSecurityToken(Configuration, userView.Username);

            return userView;
        }
    }
}