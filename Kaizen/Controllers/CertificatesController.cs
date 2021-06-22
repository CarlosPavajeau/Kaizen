using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Core.Pdf;
using Kaizen.Domain.Repositories;
using Kaizen.Models.Certificate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CertificatesController : ControllerBase
    {
        private readonly ICertificatesRepository _certificatesRepository;
        private readonly IPdfGenerator _pdfGenerator;
        private readonly IMapper _mapper;

        public CertificatesController(ICertificatesRepository certificatesRepository, IPdfGenerator pdfGenerator,
            IMapper mapper)
        {
            _certificatesRepository = certificatesRepository;
            _pdfGenerator = pdfGenerator;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CertificateViewModel>> GetCertificate(int id)
        {
            var certificate = await _certificatesRepository.FindByIdAsync(id);
            if (certificate is null)
            {
                return NotFound($"No existe un certificado con el id {id}");
            }

            return _mapper.Map<CertificateViewModel>(certificate);
        }

        [HttpGet("Client/{clientId}")]
        public async Task<ActionResult<IEnumerable<CertificateViewModel>>> GetClientCertificates(string clientId)
        {
            var certificates = await _certificatesRepository.GetClientCertificates(clientId);

            return Ok(_mapper.Map<IEnumerable<CertificateViewModel>>(certificates));
        }

        [HttpGet("[action]/{id:int}")]
        public async Task<ActionResult<FileStream>> Download(int id)
        {
            var certificate = await _certificatesRepository.FindByIdAsync(id);
            if (certificate is null)
            {
                return NotFound($"No existe un certificado con el id {id}");
            }

            var serviceNames = certificate.WorkOrder.Activity.ActivitiesServices
                .Select(a => a.Service).Select(s => s.Name);

            var stream = _pdfGenerator.GenerateCertificate(certificate.Id,
                certificate.WorkOrder.Activity.Client.TradeName, certificate.WorkOrder.Activity.Client.NIT,
                certificate.WorkOrder.ExecutionDate, string.Join(", ", serviceNames));
            stream.Position = 0;

            return File(stream, "application/octet-stream");
        }
    }
}
