using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Core.Exceptions;
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

namespace Kaizen.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(
            IApplicationUserRepository userRepository,
            IConfiguration configuration,
            IMapper mapper
            )
        {
            _userRepository = userRepository;
            Configuration = configuration;
            _mapper = mapper;
        }

        private IConfiguration Configuration { get; }

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserViewModel>> GetUser(string id)
        {
            ApplicationUser user = await _userRepository.FindByIdAsync(id);
            if (user == null)
                throw new UserDoesNotExists();
            return _mapper.Map<ApplicationUserViewModel>(user);
        }

        [HttpGet("[action]/{username}")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> CheckUserExists(string username)
        {
            return (await _userRepository.FindByNameAsync(username)) != null;
        }

        // PUT: api/Users/{id}?token={token}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, [FromQuery] string token, [FromBody] ApplicationUserEditModel editModel)
        {
            ApplicationUser user = await _userRepository.FindByIdAsync(id);

            if (user != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Users
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ApplicationUserViewModel>> PostUser([FromBody]ApplicationUserInputModel applicationUserModel)
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(applicationUserModel);

            IdentityResult result = await _userRepository.CreateAsync(user, applicationUserModel.Password);
            if (result.Succeeded)
                return GenerateAuthenticateUser(user);
            else
                throw new UserNotCreate();
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult<ApplicationUserViewModel>> Login([FromBody] LoginRequest login)
        {
            ApplicationUser user = await _userRepository.FindByNameOrEmailAsync(login.UsernameOrEmail);
            if (user is null)
                throw new UserDoesNotExists();

            Microsoft.AspNetCore.Identity.SignInResult result = await _userRepository.Login(user, login.Password);

            return (result.Succeeded) ? GenerateAuthenticateUser(user) : throw new IncorrectPassword();
        }

        private ActionResult<ApplicationUserViewModel> GenerateAuthenticateUser(ApplicationUser user)
        {
            ApplicationUserViewModel userView = _mapper.Map<ApplicationUserViewModel>(user);

            userView.Token = JwtSecurityTokenGenerator.GenerateSecurityToken(Configuration, userView.Username);

            return userView;
        }
    }
}
