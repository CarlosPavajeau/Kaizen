using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.Equipment;
using Kaizen.Test.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Kaizen.Test.Controllers
{
    [TestFixture]
    public class EquipmentsControllerTest : BaseControllerTest
    {
        private EquipmentsController _equipmentsController;
        private Mock<IEquipmentsRepository> _equipmentsRepository;
        private Mock<IUnitWork> _unitWork;

        [SetUp]
        public void SetUp()
        {
            _equipmentsRepository = new Mock<IEquipmentsRepository>();
            _unitWork = new Mock<IUnitWork>();

            _equipmentsController = new EquipmentsController(_equipmentsRepository.Object, _unitWork.Object,
                ServiceProvider.GetService<IMapper>());

            SetUpEquipmentsRepository();
            SetUpUnitWork();
        }

        private void SetUpEquipmentsRepository()
        {
            _equipmentsRepository.Setup(r => r.GetAll()).Returns(new TestAsyncEnumerable<Equipment>(new List<Equipment>
            {
                new()
                {
                    Code = "EER2",
                    Name = "Rociador"
                },
                new()
                {
                    Code = "FFR3",
                    Name = "Tijeras"
                }
            }));

            _equipmentsRepository.Setup(r => r.FindByIdAsync("EER2")).ReturnsAsync(new Equipment
            {
                Code = "EER2",
                Name = "Rociador"
            });
            _equipmentsRepository.Setup(r => r.FindByIdAsync("FF23")).ReturnsAsync((Equipment)null);

            _equipmentsRepository.Setup(r => r.Update(It.IsAny<Equipment>())).Verifiable();
            _equipmentsRepository.Setup(r => r.Insert(It.IsAny<Equipment>())).Verifiable();
        }

        private void SetUpUnitWork()
        {
            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);
        }

        [Test]
        public async Task Get_All_Equipments()
        {
            OkObjectResult result = (await _equipmentsController.GetEquipments()).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task Get_Existing_Equipment()
        {
            ActionResult<EquipmentViewModel> result = await _equipmentsController.GetEquipment("EER2");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task Get_Non_Existent_Equipment()
        {
            var result = await _equipmentsController.GetEquipment("FF23");

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task Check_If_Equipment_Exists()
        {
            bool result = await _equipmentsController.CheckExists("EER2");

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task Update_Existing_Equipment()
        {
            ActionResult<EquipmentViewModel> result = await _equipmentsController.PutEquipment("EER2", new EquipmentEditModel
            {
                Name = "Rociador de pesticida"
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual("Rociador de pesticida", result.Value.Name);
        }

        [Test]
        public async Task Update_Non_Existent_Equipment()
        {
            ActionResult<EquipmentViewModel> result = await _equipmentsController.PutEquipment("FF23", new EquipmentEditModel
            {
                Name = "Rociador de pesticida"
            });

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Save_New_Equipment()
        {
            ActionResult<EquipmentViewModel> result = await _equipmentsController.PostEquipment(new EquipmentInputModel
            {
                Code = "EERT5",
                Name = "Rociador"
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task Save_Existing_Equipment()
        {
            _unitWork.Setup(r => r.SaveAsync()).Throws(new DbUpdateException());

            ActionResult<EquipmentViewModel> result = await _equipmentsController.PostEquipment(new EquipmentInputModel
            {
                Code = "EER2",
                Name = "Rociador"
            });

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<ConflictObjectResult>(result.Result);
        }
    }
}
