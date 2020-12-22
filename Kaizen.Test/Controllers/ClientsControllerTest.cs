using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.ApplicationUser;
using Kaizen.Models.Client;
using Kaizen.Test.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Kaizen.Test.Controllers
{
    public class ClientsControllerTest : BaseControllerTest
    {
        private ClientsController _clientsController;
        private Mock<IClientsRepository> _clientsRepository;
        private Mock<IApplicationUserRepository> _applicationUserRepository;
        private Mock<IUnitWork> _unitWork;

        [SetUp]
        public void SetUp()
        {
            _clientsRepository = new Mock<IClientsRepository>();
            _applicationUserRepository = new Mock<IApplicationUserRepository>();
            _unitWork = new Mock<IUnitWork>();

            _clientsController = new ClientsController(_clientsRepository.Object, _applicationUserRepository.Object,
                _unitWork.Object, ServiceProvider.GetService<IMapper>());
        }

        [Test]
        public async Task GetClients()
        {
            _clientsRepository.Setup(r => r.GetAll()).Returns(new TestAsyncEnumerable<Client>(new[]
            {
                new Client
                {
                    Id = "1007870945",
                    FirstName = "Manolo"
                },
                new Client
                {
                    Id = "1007870946",
                    FirstName = "Juan"
                }
            }).AsQueryable());

            OkObjectResult result = (await _clientsController.GetClients()).Result as OkObjectResult;
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<ClientViewModel>>(result.Value);

            IEnumerable<ClientViewModel> value = result.Value as IEnumerable<ClientViewModel>;
            Assert.NotNull(value);
            Assert.AreEqual(2, value.Count());
        }

        [Test]
        public async Task GetClientsRequests()
        {
            _clientsRepository.Setup(r => r.GetClientRequestsAsync()).ReturnsAsync(new[]
            {
                new Client
                {
                    Id = "10078709321",
                    FirstName = "Juan",
                    State = ClientState.Pending
                },
                new Client
                {
                    Id = "10078709331",
                    FirstName = "Pedro",
                    State = ClientState.Pending
                }
            });

            OkObjectResult result = (await _clientsController.Requests()).Result as OkObjectResult;
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<ClientViewModel>>(result.Value);

            IEnumerable<ClientViewModel> value = result.Value as IEnumerable<ClientViewModel>;
            Assert.NotNull(value);
            Assert.AreEqual(2, value.Count());
        }

        [Test]
        public async Task GetClient()
        {
            _clientsRepository.Setup(r => r.FindByIdAsync("1007870945")).ReturnsAsync(new Client
            {
                Id = "1007870945",
                FirstName = "Manolo",
                LastName = "Perez"
            });

            ActionResult<ClientViewModel> result = await _clientsController.GetClient("1007870945");

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.AreEqual("1007870945", result.Value.Id);
        }

        [Test]
        public async Task PutClient()
        {
            _clientsRepository.Setup(r => r.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new Client
            {
                Id = "1007870945",
                FirstName = "Manolo",
                LastName = "Perez"
            });
            _clientsRepository.Setup(r => r.Update(It.IsAny<Client>())).Verifiable();
            _unitWork.Setup(u => u.SaveAsync()).Returns(Task.CompletedTask);

            ActionResult<ClientViewModel> result = await _clientsController.PutClient("1007870945", new ClientEditModel
            {
                FirstName = "Pedro",
                LastName = "Lopez"
            });

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.AreEqual("Pedro", result.Value.FirstName);
        }

        [Test]
        public async Task PostClient()
        {
            _clientsRepository.Setup(r => r.Insert(It.IsAny<Client>())).Verifiable();
            _applicationUserRepository.Setup(r => r.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _applicationUserRepository.Setup(r => r.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Client"))
                .ReturnsAsync(IdentityResult.Success);

            _unitWork.Setup(u => u.SaveAsync()).Returns(Task.CompletedTask);

            var result = await _clientsController.PostClient(new ClientInputModel
            {
                Id = "1007870945",
                FirstName = "Manolo",
                LastName = "Perez",
                User = new ApplicationUserInputModel
                {
                    Username = "manolos",
                    Password = "ThisIsASecurePassword",
                    Email = "client@client.com"
                }
            });

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.AreEqual("1007870945", result.Value.Id);
        }
    }
}
