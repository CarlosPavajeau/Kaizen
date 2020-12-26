using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.Equipment;
using Kaizen.Test.Helpers;
using Microsoft.AspNetCore.Mvc;
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
        }

        [Test]
        public async Task GetEquipments()
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

            OkObjectResult result = (await _equipmentsController.GetEquipments()).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }

        [Test]
        public async Task GetEquipment()
        {
            _equipmentsRepository.Setup(r => r.FindByIdAsync("EER2")).ReturnsAsync(new Equipment
            {
                Code = "EER2",
                Name = "Rociador"
            });

            ActionResult<EquipmentViewModel> result = await _equipmentsController.GetEquipment("EER2");

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }

        [Test]
        public async Task CheckExists()
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

            bool result = await _equipmentsController.CheckExists("EER2");

            Assert.NotNull(result);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task PutEquipment()
        {
            _equipmentsRepository.Setup(r => r.FindByIdAsync("EER2")).ReturnsAsync(new Equipment
            {
                Code = "EER2",
                Name = "Rociador"
            });
            _equipmentsRepository.Setup(r => r.Update(It.IsAny<Equipment>())).Verifiable();
            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            ActionResult<EquipmentViewModel> result = await _equipmentsController.PutEquipment("EER2", new EquipmentEditModel
            {
                Name = "Rociador de pesticida"
            });

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.AreEqual("Rociador de pesticida", result.Value.Name);
        }

        [Test]
        public async Task PostEquipment()
        {
            _equipmentsRepository.Setup(r => r.Insert(It.IsAny<Equipment>())).Verifiable();
            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            var result = await _equipmentsController.PostEquipment(new EquipmentInputModel()
            {
                Code = "EERT5",
                Name = "Rociador"
            });

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }
    }
}
