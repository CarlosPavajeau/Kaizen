using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Models.Employee;

namespace Kaizen.Mappers
{
    public class EmployeeMapperProfile : Profile
    {
        public EmployeeMapperProfile()
        {
            CreateMap<EmployeeEditModel, Employee>();
            CreateMap<EmployeeInputModel, Employee>();
            CreateMap<Employee, EmployeeViewModel>();

            CreateMap<EmployeeChargeInputModel, EmployeeCharge>();
            CreateMap<EmployeeCharge, EmployeeChargeViewModel>();

            CreateMap<EmployeeContract, EmployeeContractModel>();
            CreateMap<EmployeeContractModel, EmployeeContract>();
        }
    }
}
