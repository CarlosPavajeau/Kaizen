﻿using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.EditModels;
using Kaizen.InputModels;
using Kaizen.ViewModels;

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

            CreateMap<ClientInputModel, Client>();
            CreateMap<ClientAddressInputModel, ClientAddress>();
            CreateMap<ContactPersonInputModel, ContactPerson>();

            CreateMap<ClientEditModel, Client>();

            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<EmployeeCharge, EmployeeChargeViewModel>();

            CreateMap<EmployeeInputModel, Employee>();
            CreateMap<EmployeeEditModel, Employee>();
        }
    }
}
