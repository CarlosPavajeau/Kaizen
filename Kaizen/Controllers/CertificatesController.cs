using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public CertificatesController(ICertificatesRepository certificatesRepository, IMapper mapper)
        {
            _certificatesRepository = certificatesRepository;
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
    }
}
