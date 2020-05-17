using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Models.ApplicationUser;
using Kaizen.Models.Client;
using Kaizen.Models.Employee;
using Kaizen.Models.Equipment;
using Kaizen.Models.Product;
using Kaizen.Models.Service;

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
        }
    }
}
