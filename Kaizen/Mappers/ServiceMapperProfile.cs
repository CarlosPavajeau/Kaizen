using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Models.Service;

namespace Kaizen.Mappers
{
    public class ServiceMapperProfile : Profile
    {
        public ServiceMapperProfile()
        {
            CreateMap<ServiceEditModel, Service>();
            CreateMap<ServiceInputModel, Service>();
            CreateMap<ServiceType, ServiceTypeViewModel>();
            CreateMap<Service, ServiceViewModel>();
        }
    }
}
