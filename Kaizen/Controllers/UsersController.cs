using System;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Core.Exceptions.User;
using Kaizen.Core.Security;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Extensions;
using Kaizen.Middleware;
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

        public UsersController(IApplicationUserRepository userRepository, IMapper mapper,
            ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserViewModel>> GetUser(string id)
        {
            ApplicationUser user = await _userRepository.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("El usuario solicitado no se encuentra registrado");
            }

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

        [HttpPut("[action]/{id}")]
        public async Task<ActionResult<ApplicationUserViewModel>> ChangePassword(string id,
            [FromBody] ChangePasswordModel changePasswordModel)
        {
            ApplicationUser user = await _userRepository.FindByIdAsync(id);
            if (user is null)
            {
                return BadRequest($"No existe un usuario identificado con el id {id}.");
            }

            IdentityResult changePasswordResult = await _userRepository.ChangePasswordAsync(user,
                changePasswordModel.OldPassword, changePasswordModel.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                return this.IdentityResultErrors(changePasswordResult);
            }

            return _mapper.Map<ApplicationUserViewModel>(user);
        }

        [HttpPut("[action]/{usernameOrEmail}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApplicationUserViewModel>> ResetPassword(string usernameOrEmail,
            [FromBody] ResetPasswordModel resetPasswordModel)
        {
            ApplicationUser user = await _userRepository.FindByNameOrEmailAsync(usernameOrEmail);
            if (user is null)
            {
                return BadRequest(
                    $"No existe un usuario identificado con el nombre de usuario o email {usernameOrEmail}.");
            }

            IdentityResult resetPasswordResult = await _userRepository.ResetPasswordAsync(user,
                resetPasswordModel.Token.Base64ForUrlDecode(), resetPasswordModel.NewPassword);
            if (!resetPasswordResult.Succeeded)
            {
                return this.IdentityResultErrors(resetPasswordResult);
            }

            return _mapper.Map<ApplicationUserViewModel>(user);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult<ApplicationUserViewModel>> Login([FromBody] LoginRequest loginRequest)
        {
            ApplicationUser user = await _userRepository.FindByNameOrEmailAsync(loginRequest.UsernameOrEmail);
            if (user is null)
            {
                return NotFound($"El usuario/email {loginRequest.UsernameOrEmail} no se encuentra registrado.");
            }

            Microsoft.AspNetCore.Identity.SignInResult result =
                await _userRepository.Login(user, loginRequest.Password, loginRequest.IsPersistent);
            if (!result.Succeeded)
            {
                return BadRequest("Contraseña de acceso incorrecta.");
            }

            return await GenerateAuthenticateUser(user);
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<bool>> Logout()
        {
            await _userRepository.Logout();
            return true;
        }

        private async Task<ActionResult<ApplicationUserViewModel>> GenerateAuthenticateUser(ApplicationUser user)
        {
            ApplicationUserViewModel userView = _mapper.Map<ApplicationUserViewModel>(user);
            string role = await _userRepository.GetUserRoleAsync(user);

            userView.Token = _tokenGenerator.GenerateToken(user.Id, user.UserName, role);

            return userView;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<ApplicationUserViewModel>> ConfirmEmail([FromQuery] string token,
            [FromQuery] string email)
        {
            ApplicationUser user = await _userRepository.FindByNameOrEmailAsync(email);
            if (user is null)
            {
                return NotFound($"El usuario/email {email} no se encuentra registrado.");
            }

            user = await _userRepository.ConfirmEmailAsync(user, token.Base64ForUrlDecode());
            return _mapper.Map<ApplicationUserViewModel>(user);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> ForgottenPassword([FromQuery] string usernameOrEmail)
        {
            ApplicationUser user = await _userRepository.FindByNameOrEmailAsync(usernameOrEmail);
            if (user is null)
            {
                return BadRequest(
                    $"No existe un usuario identificado con el nombre de usuario o email {usernameOrEmail}.");
            }

            string token = await _userRepository.GeneratePasswordResetTokenAsync(user);

            UriBuilder uriBuilder = new UriBuilder(KaizenHttpContext.BaseUrl)
            {
                Path = "user/ResetPassword",
                Query = $"token={token.Base64ForUrlEncode()}&email={user.Email}"
            };
            string resetPasswordLink = uriBuilder.Uri.ToString();

            return await _userRepository.SendPasswordResetTokenAsync(user, resetPasswordLink);
        }
    }
}
