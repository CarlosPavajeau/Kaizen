using System.Collections.Generic;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Models.ApplicationUser;
using Kaizen.Models.Client;
using Kaizen.Models.Employee;
using Kaizen.Models.Equipment;
using Kaizen.Models.Product;
using Kaizen.Models.Service;
using Kaizen.Models.ServiceRequest;

namespace Kaizen.Mappers
{
    public class ApiAutoMapperProfile : Profile
    {
        public ApiAutoMapperProfile()
        {
            CreateMap<Client, ClientViewModel>();
            CreateMap<ClientAddress, ClientAddressViewModel>();
            CreateMap<ContactPerson, ContactPersonViewModel>();

            CreateMap<ApplicationUser, ApplicationUserViewModel>();
            CreateMap<ApplicationUserInputModel, ApplicationUser>();

            CreateMap<ClientInputModel, Client>();
            CreateMap<ClientAddressInputModel, ClientAddress>();
            CreateMap<ContactPersonInputModel, ContactPerson>();

            CreateMap<ClientEditModel, Client>();

            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<EmployeeCharge, EmployeeChargeViewModel>();

            CreateMap<EmployeeInputModel, Employee>();
            CreateMap<EmployeeEditModel, Employee>();

            CreateMap<Equipment, EquipmentViewModel>();
            CreateMap<EquipmentEditModel, Equipment>();
            CreateMap<EquipmentInputModel, Equipment>();

            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductEditModel, Product>();
            CreateMap<ProductInputModel, Product>();

            CreateMap<ServiceType, ServiceTypeViewModel>();
            CreateMap<ServiceEditModel, Service>();
            CreateMap<ServiceInputModel, Service>();
            CreateMap<Service, ServiceViewModel>();

            CreateMap<ServiceRequestEditModel, ServiceRequest>();
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
