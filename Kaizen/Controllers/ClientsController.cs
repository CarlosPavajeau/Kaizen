using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using Kaizen.Extensions;
using Kaizen.Models.Client;
using Microsoft.AspNetCore.Authorization;
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

        public ClientsController(IClientsRepository clientsRepository, IApplicationUserRepository applicationUserRepository, IUnitWork unitWork, IMapper mapper)
        {
            _clientsRepository = clientsRepository;
            _applicationUserRepository = applicationUserRepository;
            _unitWork = unitWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientViewModel>>> GetClients()
        {
            List<Client> clients = await _clientsRepository.GetAll().ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ClientViewModel>>(clients));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ClientViewModel>>> Requests()
        {
            IEnumerable<Client> clients = await _clientsRepository.GetClientRequestsAsync();
            return Ok(_mapper.Map<IEnumerable<ClientViewModel>>(clients));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientViewModel>> GetClient(string id)
        {
            Client client = await _clientsRepository.FindByIdAsync(id);
            if (client == null)
            {
                return NotFound($"El cliente con identificación {id} no se encuentra registrado.");
            }

            return _mapper.Map<ClientViewModel>(client);
        }

        [HttpGet("[action]/{userId}")]
        public async Task<ActionResult<string>> ClientId(string userId)
        {
            return await _clientsRepository.GetClientId(userId);
        }

        [HttpGet("[action]/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> CheckExists(string id)
        {
            return await _clientsRepository.GetAll().AnyAsync(c => c.Id == id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClientViewModel>> PutClient(string id, ClientEditModel clientModel)
        {
            Client client = await _clientsRepository.FindByIdAsync(id);
            if (client is null)
            {
                return BadRequest($"El cliente con identificación {id} no se encuentra registrado.");
            }

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

                throw;
            }

            return _mapper.Map<ClientViewModel>(client);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ClientViewModel>> PostClient(ClientInputModel clientInput)
        {
            Client client = _mapper.Map<Client>(clientInput);

            IdentityResult result = await _applicationUserRepository.CreateAsync(client.User, clientInput.User.Password);
            if (!result.Succeeded)
            {
                return this.IdentityResultErrors(result);
            }

            IdentityResult roleResult = await _applicationUserRepository.AddToRoleAsync(client.User, "Client");
            if (!roleResult.Succeeded)
            {
                return this.IdentityResultErrors(roleResult);
            }

            client.PublishEvent(new SavedPerson(client));
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

                throw;
            }

            return _mapper.Map<ClientViewModel>(client);
        }

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
