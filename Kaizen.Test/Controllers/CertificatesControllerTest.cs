using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Models.Certificate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Kaizen.Test.Controllers
{
    public class CertificatesControllerTest : BaseControllerTest
    {
        private CertificatesController _certificatesController;
        private Mock<ICertificatesRepository> _certificatesRepository;

        [SetUp]
        public void SetUp()
        {
            _certificatesRepository = new Mock<ICertificatesRepository>();

            _certificatesController =
                new CertificatesController(_certificatesRepository.Object, ServiceProvider.GetService<IMapper>());

            SetUpCertificatesRepository();
        }

        private void SetUpCertificatesRepository()
        {
            _certificatesRepository.Setup(r => r.GetClientCertificates("1007870922")).ReturnsAsync(
                new List<Certificate>
                {
                    new()
                    {
                        Id = 1,
                        Validity = DateTime.Now.AddDays(10),
                        WorkOrderCode = 1
                    },
                    new()
                    {
                        Id = 2,
                        Validity = DateTime.Now.AddDays(15),
                        WorkOrderCode = 2
                    }
                });

            _certificatesRepository.Setup(r => r.FindByIdAsync(1)).ReturnsAsync(new Certificate
            {
                Id = 1,
                Validity = DateTime.Now.AddDays(10),
                WorkOrderCode = 1
            });
            _certificatesRepository.Setup(r => r.FindByIdAsync(3)).ReturnsAsync((Certificate) null);
        }

        [Test]
        public async Task Get_Client_Certificates()
        {
            var result = (await _certificatesController.GetClientCertificates("1007870922")).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<IEnumerable<CertificateViewModel>>(result.Value);

            var value = result.Value as IEnumerable<CertificateViewModel>;
            Assert.IsNotNull(value);
            Assert.AreEqual(2, value.Count());
        }

        [Test]
        public async Task Get_Existing_Certificate()
        {
            var result = await _certificatesController.GetCertificate(1);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(1, result.Value.Id);
        }

        [Test]
        public async Task Get_Non_Existent_Certificate()
        {
            var result = await _certificatesController.GetCertificate(3);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);

            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }
    }
}
