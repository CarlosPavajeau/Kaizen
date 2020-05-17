using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Core.Exceptions.Client;
using Kaizen.Core.Exceptions.User;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
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
            return await _clientsRepository.GetAll().Select(c => _mapper.Map<ClientViewModel>(c)).ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientViewModel>> GetClient(string id)
        {
            Client client = await _clientsRepository.FindByIdAsync(id);
            if (client == null)
                throw new ClientNotRegister();

            ClientViewModel clientViewModel = _mapper.Map<ClientViewModel>(client);
            return clientViewModel;
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ClientAddressViewModel>> ClientAddress(string id)
        {
            ClientAddress clientAddress = await _clientsRepository.GetClientAddressAsync(id);
            if (clientAddress is null)
                throw new ClientNotRegister();

            return _mapper.Map<ClientAddressViewModel>(clientAddress);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<IEnumerable<ContactPersonViewModel>>> ClientContactPeople(string id)
        {
            var contactPeople = await _clientsRepository.GetClientContactPeopleAsync(id);

            return contactPeople.Select(c => _mapper.Map<ContactPersonViewModel>(c)).ToList();
        }

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
            Client client = _clientsRepository.FindById(id);
            if (client is null)
                throw new ClientNotRegister();

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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return _mapper.Map<ClientViewModel>(client);
        }

        [HttpPut("ClientAddress/{id}")]
        public async Task<ActionResult<ClientAddressViewModel>> PutClientAddress(string id, ClientAddressEditModel clientAddressEdit)
        {
            ClientAddress clientAddress = await _clientsRepository.GetClientAddressAsync(id);
            if (clientAddress is null)
                throw new ClientNotRegister();

            _mapper.Map(clientAddressEdit, clientAddress);
            _clientsRepository.UpdateClientAddress(clientAddress);

            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return _mapper.Map<ClientAddressViewModel>(clientAddress);
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
            _clientsRepository.Insert(client);

            try
            {
                await _unitWork.SaveAsync();
            }
            catch (DbUpdateException)
            {
                if (ClientExists(clientInput.Id))
                {
                    throw new ClientAlreadyRegistered(client.Id);
                }
                else
                {
                    throw new ClientNotRegister();
                }
            }

            return _mapper.Map<ClientViewModel>(client);
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
