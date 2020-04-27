using Kaizen.Domain.Entities;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserViewModel>> GetUser(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();
            return new ApplicationUserViewModel(user);
        }

        [HttpGet("[action]/{username}")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> CheckUserExists(string username)
        {
            return await _userManager.FindByNameAsync(username) != null;
        }

        // PUT: api/Users/{id}?token={token}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, [FromQuery] string token, [FromBody] ApplicationUserEditModel editModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);

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

                IdentityResult result = await _userManager.CreateAsync(user, inputModel.Password);
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
                ApplicationUser user = await _userManager.FindByNameAsync(login.UsernameOrEmail);
                if (user is null)
                {
                    user = await _userManager.FindByEmailAsync(login.UsernameOrEmail);
                    if (user is null)
                        return NotFound();
                }

                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
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