using System.Collections.Generic;
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

            CreateMap<ServiceInputModel, Service>().AfterMap((serviceModel, service) =>
            {
                service.EquipmentsServices = new List<EquipmentService>();
                service.ProductsServices = new List<ProductService>();
                service.EmployeesServices = new List<EmployeeService>();
                foreach (var equipmentCode in serviceModel.EquipmentCodes)
                {
                    service.EquipmentsServices.Add(new EquipmentService { EquipmentCode = equipmentCode, Service = service });
                }
                foreach (var productCode in serviceModel.ProductCodes)
                {
                    service.ProductsServices.Add(new ProductService { ProductCode = productCode, Service = service });
                }
                foreach (var employeeCode in serviceModel.EmployeeCodes)
                {
                    service.EmployeesServices.Add(new EmployeeService { EmployeeId = employeeCode, Service = service });
                }
            });

            CreateMap<ServiceType, ServiceTypeViewModel>();
            CreateMap<Service, ServiceViewModel>();
        }
    }
}
