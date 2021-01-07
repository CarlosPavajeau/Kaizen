using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.Service;
using Kaizen.Test.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Kaizen.Test.Controllers
{
    [TestFixture]
    public class ServicesControllerTest : BaseControllerTest
    {
        private ServicesController _servicesController;
        private Mock<IServicesRepository> _servicesRepository;
        private Mock<IUnitWork> _unitWork;

        [SetUp]
        public void SetUp()
        {
            _servicesRepository = new Mock<IServicesRepository>();
            _unitWork = new Mock<IUnitWork>();

            _servicesController = new ServicesController(_servicesRepository.Object, _unitWork.Object,
                ServiceProvider.GetService<IMapper>());

            SetUpServicesRepository();
            SetUpUnitWork();
        }

        private void SetUpServicesRepository()
        {
            _servicesRepository.Setup(r => r.GetAll()).Returns(new TestAsyncEnumerable<Service>(new List<Service>
            {
                new ()
                {
                    Code = "EERG",
                    Name = "Control de plagas",
                    Cost = 50000
                },
                new ()
                {
                    Code = "FFGT",
                    Name = "Limpieza",
                    Cost = 50000
                }
            }).AsQueryable());

            _servicesRepository.Setup(r => r.GetServiceTypesAsync()).ReturnsAsync(new List<ServiceType>
            {
                new ()
                {
                    Id = 1,
                    Name = "Control de plagas"
                },
                new ()
                {
                    Id = 2,
                    Name = "Limpieza de espacios"
                }
            });

            _servicesRepository.Setup(r => r.FindByIdAsync("EERG")).ReturnsAsync(new Service
            {
                Code = "EERG",
                Name = "Control de plagas",
                Cost = 50000
            });
            _servicesRepository.Setup(r => r.FindByIdAsync("GGT3")).ReturnsAsync((Service)null);

            _servicesRepository.Setup(r => r.Update(It.IsAny<Service>())).Verifiable();

            _servicesRepository.Setup(r => r.Insert(It.IsAny<Service>())).Verifiable();
        }

        private void SetUpUnitWork()
        {
            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);
        }

        [Test]
        public async Task Get_All_Services()
        {
            OkObjectResult result = (await _servicesController.GetServices()).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<ServiceViewModel>>(result.Value);
        }

        [Test]
        public async Task Get_Existing_Service()
        {
            ActionResult<ServiceViewModel> result = await _servicesController.GetService("EERG");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual("EERG", result.Value.Code);
        }

        [Test]
        public async Task Get_Non_Existent_Service()
        {
            ActionResult<ServiceViewModel> result = await _servicesController.GetService("GGT3");

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task Get_ServiceTypes()
        {
            OkObjectResult result = (await _servicesController.ServiceTypes()).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<ServiceTypeViewModel>>(result.Value);
        }

        [Test]
        public async Task Check_Service_Exists()
        {
            bool result = await _servicesController.CheckExists("EERG");

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task Update_Existing_Service()
        {
            ActionResult<ServiceViewModel> result = await _servicesController.PutService("EERG", new ServiceEditModel
            {
                Name = "Control de plagas voladoras",
                Cost = 30000
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual("Control de plagas voladoras", result.Value.Name);
        }

        [Test]
        public async Task Update_Non_Existent_Service()
        {
            ActionResult<ServiceViewModel> result = await _servicesController.PutService("GGT3", new ServiceEditModel
            {
                Name = "Control de plagas voladoras",
                Cost = 30000
            });

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Save_New_Service()
        {
            ActionResult<ServiceViewModel> result = await _servicesController.PostService(new ServiceInputModel
            {
                Code = "GGT3",
                Name = "Control de plagas",
                Cost = 35000,
                EmployeeCodes = new List<string> { "1007870919" },
                EquipmentCodes = new List<string> { "ERH6" },
                ProductCodes = new List<string> { "33SQ" }
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual("GGT3", result.Value.Code);
        }

        [Test]
        public async Task Save_Existing_Service()
        {
            _unitWork.Setup(r => r.SaveAsync()).Throws(new DbUpdateException());

            ActionResult<ServiceViewModel> result = await _servicesController.PostService(new ServiceInputModel
            {
                Code = "EERG",
                Name = "Control de plagas",
                Cost = 35000,
                EmployeeCodes = new List<string> { "1007870919" },
                EquipmentCodes = new List<string> { "ERH6" },
                ProductCodes = new List<string> { "33SQ" }
            });

            Assert.NotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<ConflictObjectResult>(result.Result);
        }
    }
}
