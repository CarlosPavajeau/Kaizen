using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.EditModels;
using Kaizen.InputModels;
using Kaizen.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kaizen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientViewModel>>> GetClients()
        {
            return await _context.Clients.Select(c => new ClientViewModel(c)).ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientViewModel>> GetClient(string id)
        {
            Client client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return new ClientViewModel(client);
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(string id, ClientEditModel clientModel)
        {
            Client client = await _context.Clients.FindAsync(id);

            if (client is null)
            {
                return BadRequest();
            }

            client.FirstName = clientModel.FirstName;
            client.SecondName = clientModel.SecondName;
            client.LastName = clientModel.LastName;
            client.SeconLastname = clientModel.SeconLastname;
            client.ClientType = clientModel.ClientType;
            client.BusninessName = clientModel.BusninessName;
            client.TradeName = clientModel.TradeName;
            client.FirstPhoneNumber = clientModel.FirstPhoneNumber;
            client.SecondPhoneNumber = clientModel.SecondPhoneNumber;
            client.FirstLandLine = clientModel.FirstLandLine;
            client.SecondLandLine = clientModel.SecondLandLine;

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        // POST: api/Clients
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ClientViewModel>> PostClient(ClientInputModel clientInput)
        {
            ApplicationUser user = await _context.Users.FindAsync(clientInput.UserId);
            if (user is null)
            {
                return BadRequest();
            }

            Client client = new Client
            {
                Id = clientInput.Id,
                FirstName = clientInput.FirstName,
                SecondName = clientInput.SecondName,
                LastName = clientInput.LastName,
                SeconLastname = clientInput.SeconLastname,
                ClientType = clientInput.ClientType,
                BusninessName = clientInput.BusninessName,
                TradeName = clientInput.TradeName,
                FirstPhoneNumber = clientInput.FirstPhoneNumber,
                SecondPhoneNumber = clientInput.SecondPhoneNumber,
                FirstLandLine = clientInput.FirstLandLine,
                SecondLandLine = clientInput.SecondLandLine,
                ClientAddress = new ClientAddress
                {
                    City = clientInput.ClientAddress.City,
                    Neighborhood = clientInput.ClientAddress.Neighborhood,
                    Street = clientInput.ClientAddress.Street,
                    ClientId = clientInput.Id
                },
                ContactPeople = clientInput.ContactPeople.Select(c => new ContactPerson
                {
                    Name = c.Name,
                    PhoneNumber = c.Phonenumber
                }).ToList(),
                User = user
            };

            _context.Clients.Add(client);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClientExists(clientInput.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return new ClientViewModel(client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClientViewModel>> DeleteClient(string id)
        {
            Client client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return new ClientViewModel(client);
        }

        private bool ClientExists(string id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
