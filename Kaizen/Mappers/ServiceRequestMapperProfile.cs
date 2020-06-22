using System.Collections.Generic;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Models.ServiceRequest;

namespace Kaizen.Mappers
{
    public class ServiceRequestMapperProfile : Profile
    {
        public ServiceRequestMapperProfile()
        {
            CreateMap<ServiceRequestInputModel, ServiceRequest>().AfterMap((serviceRequestModel, serviceRequest) =>
            {
                serviceRequest.ServiceRequestsServices = new List<ServiceRequestService>();
                foreach (var serviceCode in serviceRequestModel.ServiceCodes)
                {
                    serviceRequest.ServiceRequestsServices.Add(new ServiceRequestService
                    {
                        ServiceCode = serviceCode,
                        ServiceRequest = serviceRequest
                    });
                }
            });
            CreateMap<ServiceRequest, ServiceRequestViewModel>();
        }
    }
}
