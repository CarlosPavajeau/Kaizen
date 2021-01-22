using System.Collections.Generic;
using System.Linq;
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
                foreach (string equipmentCode in serviceModel.EquipmentCodes)
                {
                    service.EquipmentsServices.Add(new EquipmentService { EquipmentCode = equipmentCode, Service = service });
                }
                foreach (string productCode in serviceModel.ProductCodes)
                {
                    service.ProductsServices.Add(new ProductService { ProductCode = productCode, Service = service });
                }
                foreach (string employeeCode in serviceModel.EmployeeCodes)
                {
                    service.EmployeesServices.Add(new EmployeeService { EmployeeId = employeeCode, Service = service });
                }
            });

            CreateMap<ServiceType, ServiceTypeViewModel>();
            CreateMap<Service, ServiceViewModel>().BeforeMap((service, _) =>
            {
                if (service is null)
                {
                    return;
                }

                if (service.EmployeesServices != null)
                {
                    service.Employees = service.EmployeesServices.Select(s => s.Employee).ToList();
                }

                if (service.EquipmentsServices != null)
                {
                    service.Equipments = service.EquipmentsServices.Select(s => s.Equipment).ToList();
                }

                if (service.ProductsServices != null)
                {
                    service.Products = service.ProductsServices.Select(s => s.Product).ToList();
                }
            });
        }
    }
}
