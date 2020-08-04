using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Core.Exceptions.User;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using Kaizen.Extensions;
using Kaizen.Models.Client;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public ClientsController(IClientsRepository clientsRepository, IUnitWork unitWork, IMapper mapper)
        {
            _clientsRepository = clientsRepository;
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
            ApplicationUser user = _unitWork.ApplicationUsers.FindById(clientInput.UserId);
            if (user is null)
                throw new UserDoesNotExists();

            Client client = _mapper.Map<Client>(clientInput);
            client.User = user;
            string emailConfirmationLink = await GenerateEmailConfirmationLink(user);

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

            return _mapper.Map<ClientViewModel>(client);
        }

        private async Task<string> GenerateEmailConfirmationLink(ApplicationUser user)
        {
            string token = await _unitWork.ApplicationUsers.GenerateEmailConfirmationTokenAsync(user);
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
