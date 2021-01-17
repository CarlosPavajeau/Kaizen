using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Infrastructure.Identity;
using Kaizen.Models.ApplicationUser;
using Kaizen.Models.Client;
using Kaizen.Test.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Kaizen.Test.Controllers
{
    [TestFixture]
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

            SetUpClientsRepository();
            SetUpApplicationUserRepository();
            SetUpUnitWork();
        }

        private void SetUpClientsRepository()
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

            _clientsRepository.Setup(r => r.FindByIdAsync("1007870945")).ReturnsAsync(new Client
            {
                Id = "1007870945",
                FirstName = "Manolo",
                LastName = "Perez"
            });
            _clientsRepository.Setup(r => r.FindByIdAsync("1007870944")).ReturnsAsync((Client)null);

            _clientsRepository.Setup(r => r.Update(It.IsAny<Client>())).Verifiable();
            _clientsRepository.Setup(r => r.Insert(It.IsAny<Client>())).Verifiable();
        }

        private void SetUpApplicationUserRepository()
        {
            _applicationUserRepository.Setup(r => r.CreateAsync(It.Is<ApplicationUser>(a => a.UserName == "manolos"),
                    It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _applicationUserRepository.Setup(r => r.CreateAsync(It.Is<ApplicationUser>(a => a.UserName == "admin"),
                    It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new SpanishIdentityErrorDescriber().DuplicateUserName("admin")));

            _applicationUserRepository.Setup(r => r.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Client"))
                .ReturnsAsync(IdentityResult.Success);
        }

        private void SetUpUnitWork()
        {
            _unitWork.Setup(u => u.SaveAsync()).Returns(Task.CompletedTask);
        }

        [Test]
        public async Task Get_All_Clients()
        {
            OkObjectResult result = (await _clientsController.GetClients()).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<ClientViewModel>>(result.Value);

            IEnumerable<ClientViewModel> value = result.Value as IEnumerable<ClientViewModel>;
            Assert.IsNotNull(value);
            Assert.AreEqual(2, value.Count());
        }

        [Test]
        public async Task Get_All_Clients_Requests()
        {
            OkObjectResult result = (await _clientsController.Requests()).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<ClientViewModel>>(result.Value);

            IEnumerable<ClientViewModel> value = result.Value as IEnumerable<ClientViewModel>;
            Assert.IsNotNull(value);
            Assert.AreEqual(2, value.Count());
        }

        [Test]
        public async Task Get_Existing_Client()
        {
            ActionResult<ClientViewModel> result = await _clientsController.GetClient("1007870945");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual("1007870945", result.Value.Id);
        }

        [Test]
        public async Task Get_Non_Existent_Client()
        {
            ActionResult<ClientViewModel> result = await _clientsController.GetClient("1007870944");

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task Update_Existing_Client()
        {
            ActionResult<ClientViewModel> result = await _clientsController.PutClient("1007870945", new ClientEditModel
            {
                FirstName = "Pedro",
                LastName = "Lopez"
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual("Pedro", result.Value.FirstName);
        }

        [Test]
        public async Task Update_Non_Existent_Client()
        {
            ActionResult<ClientViewModel> result = await _clientsController.PutClient("1007870944", new ClientEditModel
            {
                FirstName = "Pedro",
                LastName = "Lopez"
            });

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Save_New_Client_With_New_Username()
        {
            ActionResult<ClientViewModel> result = await _clientsController.PostClient(new ClientInputModel
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

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual("1007870945", result.Value.Id);
        }

        [Test]
        public async Task Save_New_Client_With_An_Existing_Username()
        {
            ActionResult<ClientViewModel> result = await _clientsController.PostClient(new ClientInputModel
            {
                Id = "1007870945",
                FirstName = "Manolo",
                LastName = "Perez",
                User = new ApplicationUserInputModel
                {
                    Username = "admin",
                    Password = "ThisIsASecurePassword",
                    Email = "client@client.com"
                }
            });

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Save_Existing_Client()
        {
            _unitWork.Setup(r => r.SaveAsync()).Throws(new DbUpdateException());

            ActionResult<ClientViewModel> result = await _clientsController.PostClient(new ClientInputModel
            {
                Id = "1007870946",
                FirstName = "Manolo",
                LastName = "Perez",
                User = new ApplicationUserInputModel
                {
                    Username = "manolos",
                    Password = "ThisIsASecurePassword",
                    Email = "client@client.com"
                }
            });

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<ConflictObjectResult>(result.Result);
        }
    }
}
