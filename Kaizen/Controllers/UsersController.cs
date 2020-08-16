using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Core.Exceptions.User;
using Kaizen.Core.Security;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Extensions;
using Kaizen.Models.ApplicationUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenGenerator _tokenGenerator;

        public UsersController(IApplicationUserRepository userRepository, IMapper mapper, ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
        }

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserViewModel>> GetUser(string id)
        {
            ApplicationUser user = await _userRepository.FindByIdAsync(id);
            if (user == null)
                return NotFound("El usuario solicitado no se encuentra registrado");

            return _mapper.Map<ApplicationUserViewModel>(user);
        }

        [HttpGet("[action]/{usernameOrEmailOrPhone}")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> CheckExists(string usernameOrEmailOrPhone)
        {
            return await _userRepository.GetAll()
                .AnyAsync(p => p.UserName == usernameOrEmailOrPhone ||
                          p.PhoneNumber == usernameOrEmailOrPhone ||
                          p.Email == usernameOrEmailOrPhone);
        }

        // PUT: api/Users/ChangePassword/{id}
        [HttpPut("[action]/{id}")]
        public async Task<ActionResult<ApplicationUserViewModel>> ChangePassword(string id, [FromBody] ChangePasswordModel changePasswordModel)
        {
            ApplicationUser user = await _userRepository.FindByIdAsync(id);
            if (user is null)
                return BadRequest($"No existe un usuario identificado con el id {id}.");

            IdentityResult changePasswordResult = await _userRepository.ChangePassswordAsync(user, changePasswordModel.OldPassword, changePasswordModel.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                SetIdentityResultErrors(changePasswordResult);
                return BadRequest(new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                });
            }

            return _mapper.Map<ApplicationUserViewModel>(user);
        }

        // POST: api/Users
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ApplicationUserViewModel>> PostUser([FromBody] ApplicationUserInputModel applicationUserModel)
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(applicationUserModel);

            IdentityResult createResult = await _userRepository.CreateAsync(user, applicationUserModel.Password);
            if (!createResult.Succeeded)
            {
                SetIdentityResultErrors(createResult);
                goto IdentityErrors;
            }

            IdentityResult roleResult = await _userRepository.AddToRoleAsync(user, applicationUserModel.Role);
            if (!roleResult.Succeeded)
            {
                SetIdentityResultErrors(roleResult);
                goto IdentityErrors;
            }

            return await GenerateAuthenticateUser(user);

        IdentityErrors:
            return BadRequest(new ValidationProblemDetails(ModelState)
            {
                Status = StatusCodes.Status400BadRequest
            });
        }

        private void SetIdentityResultErrors(IdentityResult identityResult)
        {
            foreach (IdentityError error in identityResult.Errors)
                ModelState.AddModelError(error.Code, error.Description);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult<ApplicationUserViewModel>> Login([FromBody] LoginRequest login)
        {
            ApplicationUser user = await _userRepository.FindByNameOrEmailAsync(login.UsernameOrEmail);
            if (user is null)
                return NotFound($"El usuario/email {login.UsernameOrEmail} no se encuentra registrado.");

            Microsoft.AspNetCore.Identity.SignInResult result = await _userRepository.Login(user, login.Password);
            if (!result.Succeeded)
                throw new IncorrectPassword();

            return await GenerateAuthenticateUser(user);
        }

        private async Task<ActionResult<ApplicationUserViewModel>> GenerateAuthenticateUser(ApplicationUser user)
        {
            ApplicationUserViewModel userView = _mapper.Map<ApplicationUserViewModel>(user);
            string role = await _userRepository.GetUserRoleAsync(user);

            userView.Token = _tokenGenerator.GenerateToken(user.UserName, role);

            return userView;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<ApplicationUserViewModel>> ConfirmEmail([FromQuery] string token, [FromQuery] string email)
        {
            ApplicationUser user = await _userRepository.FindByNameOrEmailAsync(email);
            if (user is null)
                return NotFound($"El usuario/email {email} no se encuentra registrado.");

            user = await _userRepository.ConfirmEmailAsync(user, token.Base64ForUrlDecode());
            return Ok(_mapper.Map<ApplicationUser>(user));
        }
    }
}
