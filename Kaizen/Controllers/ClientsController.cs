using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Core.Security;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using Kaizen.Extensions;
using Kaizen.Models.Client;
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
    public class ClientsController : ControllerBase
    {
        private readonly IClientsRepository _clientsRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;
        private readonly ITokenGenerator _tokenGenerator;

        public ClientsController(IClientsRepository clientsRepository, IApplicationUserRepository applicationUserRepository,
                                 ITokenGenerator tokenGenerator, IUnitWork unitWork, IMapper mapper)
        {
            _clientsRepository = clientsRepository;
            _applicationUserRepository = applicationUserRepository;
            _tokenGenerator = tokenGenerator;
            _unitWork = unitWork;
            _mapper = mapper;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientViewModel>>> GetClients()
        {
            List<Client> clients = await _clientsRepository.GetAll().ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ClientViewModel>>(clients));
        }

        // GET: api/Clients/Requests
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ClientViewModel>>> Requests()
        {
            IEnumerable<Client> clients = await _clientsRepository.GetClientRequestsAsync();
            return Ok(_mapper.Map<IEnumerable<ClientViewModel>>(clients));
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientViewModel>> GetClient(string id)
        {
            Client client = await _clientsRepository.FindByIdAsync(id);
            if (client == null)
                return NotFound($"El cliente con identificación {id} no se encuentra registrado.");

            return _mapper.Map<ClientViewModel>(client);
        }

        // GET: api/Clients/ClientId/{userId}
        [HttpGet("[action]/{userId}")]
        public async Task<ActionResult<string>> ClientId(string userId)
        {
            return await _clientsRepository.GetClientId(userId);
        }

        // GET: api/Clients/CheckExists/5
        [HttpGet("[action]/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> CheckExists(string id)
        {
            return await _clientsRepository.GetAll().AnyAsync(c => c.Id == id);
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<ClientViewModel>> PutClient(string id, ClientEditModel clientModel)
        {
            Client client = await _clientsRepository.FindByIdAsync(id);
            if (client is null)
                return BadRequest($"El cliente con identificación {id} no se encuentra registrado.");

            _mapper.Map(clientModel, client);
            _clientsRepository.Update(client);

            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound($"Actualización fallida. El cliente con identificación {id} no se encuentra registrado.");
                }
                else
                {
                    throw;
                }
            }

            return _mapper.Map<ClientViewModel>(client);
        }

        // POST: api/Clients
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ClientViewModel>> PostClient(ClientInputModel clientInput)
        {
            Client client = _mapper.Map<Client>(clientInput);

            IdentityResult result = await _applicationUserRepository.CreateAsync(client.User, clientInput.User.Password);
            if (!result.Succeeded)
            {
                return GetIdentityResultErrors(result);
            }

            IdentityResult roleResult = await _applicationUserRepository.AddToRoleAsync(client.User, "Client");
            if (!roleResult.Succeeded)
            {
                return GetIdentityResultErrors(roleResult);
            }

            string emailConfirmationLink = await GenerateEmailConfirmationLink(client.User);
            client.PublishEvent(new SavedPerson(client, emailConfirmationLink));

            _clientsRepository.Insert(client);

            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateException)
            {
                if (ClientExists(clientInput.Id))
                {
                    return Conflict($"El cliente con identificación {clientInput.Id} ya se encuentra registrado.");
                }
                else
                {
                    throw;
                }
            }

            ClientViewModel clientViewModel = _mapper.Map<ClientViewModel>(client);
            clientViewModel.User.Token = _tokenGenerator.GenerateToken(client.User.UserName, "Client");
            return clientViewModel;
        }

        private ActionResult GetIdentityResultErrors(IdentityResult identityResult)
        {
            foreach (IdentityError error in identityResult.Errors)
                ModelState.AddModelError(error.Code, error.Description);

            return BadRequest(new ValidationProblemDetails(ModelState)
            {
                Status = StatusCodes.Status400BadRequest
            });
        }

        private async Task<string> GenerateEmailConfirmationLink(ApplicationUser user)
        {
            string token = await _applicationUserRepository.GenerateEmailConfirmationTokenAsync(user);
            string emailConfirmationLink = Url.Action("ConfirmEmail", "user", new { token = token.Base64ForUrlEncode(), email = user.Email }, Request.Scheme);
            return emailConfirmationLink;
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClientViewModel>> DeleteClient(string id)
        {
            Client client = await _clientsRepository.FindByIdAsync(id);
            return _mapper.Map<ClientViewModel>(client);
        }

        private bool ClientExists(string id)
        {
            return _clientsRepository.GetAll().Any(c => c.Id == id);
        }
    }
}
