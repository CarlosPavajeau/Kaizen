using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.WorkOrder;
using Kaizen.Test.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Kaizen.Test.Controllers
{
    [TestFixture]
    public class WorkOrdersControllerTest : BaseControllerTest
    {
        private WorkOrdersController _workOrdersController;
        private Mock<IWorkOrdersRepository> _workOrdersRepository;
        private Mock<IUnitWork> _unitWork;

        [SetUp]
        public void SetUp()
        {
            _workOrdersRepository = new Mock<IWorkOrdersRepository>();
            _unitWork = new Mock<IUnitWork>();

            _workOrdersController = new WorkOrdersController(_workOrdersRepository.Object, _unitWork.Object,
                ServiceProvider.GetService<IMapper>());

            SetUpWorkOrdersRepository();
            SetUpUnitWork();
        }

        private void SetUpWorkOrdersRepository()
        {
            _workOrdersRepository.Setup(r => r.GetAll()).Returns(new TestAsyncEnumerable<WorkOrder>(new List<WorkOrder>
            {
                new()
                {
                    Code = 1,
                    ActivityCode = 1
                },
                new()
                {
                    Code = 2,
                    ActivityCode = 2
                }
            }));

            _workOrdersRepository.Setup(r => r.GetSectorsAsync()).ReturnsAsync(new List<Sector>
            {
                new()
                {
                    Id = 1,
                    Name = "Comercial"
                },
                new()
                {
                    Id = 1,
                    Name = "Escolar"
                }
            });

            _workOrdersRepository.Setup(r => r.FindByIdAsync(1)).ReturnsAsync(new WorkOrder
            {
                Code = 1,
                ActivityCode = 1
            });
            _workOrdersRepository.Setup(r => r.FindByIdAsync(3)).ReturnsAsync((WorkOrder)null);

            _workOrdersRepository.Setup(r => r.FindByActivityCodeAsync(1)).ReturnsAsync(new WorkOrder
            {
                Code = 1,
                ActivityCode = 1
            });
            _workOrdersRepository.Setup(r => r.FindByActivityCodeAsync(3)).ReturnsAsync((WorkOrder)null);

            _workOrdersRepository.Setup(r => r.Update(It.IsAny<WorkOrder>())).Verifiable();
            _workOrdersRepository.Setup(r => r.Insert(It.IsAny<WorkOrder>())).Verifiable();
        }

        private void SetUpUnitWork()
        {
            _unitWork.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);
        }

        [Test]
        public async Task Get_WorkOrders()
        {
            OkObjectResult result = (await _workOrdersController.GetWorkOrders()).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<WorkOrderViewModel>>(result.Value);
        }

        [Test]
        public async Task Get_Existing_WorkOrder()
        {
            ActionResult<WorkOrderViewModel> result = await _workOrdersController.GetWorkOrder(1);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task Get_Non_Existent_WorkOrder()
        {
            ActionResult<WorkOrderViewModel> result = await _workOrdersController.GetWorkOrder(3);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task Get_Existing_WorkOrder_By_Activity_Code()
        {
            ActionResult<WorkOrderViewModel> result = await _workOrdersController.ActivityWorkOrder(1);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task Get_Non_Existent_WorkOrder_By_Activity_Code()
        {
            ActionResult<WorkOrderViewModel> result = await _workOrdersController.ActivityWorkOrder(3);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public async Task Get_Sectors()
        {
            OkObjectResult result = (await _workOrdersController.Sectors()).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<SectorViewModel>>(result.Value);
        }

        [Test]
        public async Task Update_Existing_WorkOrder()
        {
            ActionResult<WorkOrderViewModel> result = await _workOrdersController.PutWorkOrder(1, new WorkOrderEditModel
            {
                WorkOrderState = WorkOrderState.Confirmed
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(WorkOrderState.Confirmed, result.Value.WorkOrderState);
        }

        [Test]
        public async Task Update_Non_Existent_WorkOrder()
        {
            ActionResult<WorkOrderViewModel> result = await _workOrdersController.PutWorkOrder(3, new WorkOrderEditModel
            {
                WorkOrderState = WorkOrderState.Confirmed
            });

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Save_New_WorkOrder()
        {
            var result = await _workOrdersController.PostWorkOrder(new WorkOrderInputModel
            {
                ActivityCode = 3,
                SectorId = 1,
                WorkOrderState = WorkOrderState.Generated
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }
    }
}
