using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Models.Certificate;

namespace Kaizen.Mappers
{
    public class CertificateMapperProfile : Profile
    {
        public CertificateMapperProfile()
        {
            CreateMap<Certificate, CertificateViewModel>();
        }
    }
}
