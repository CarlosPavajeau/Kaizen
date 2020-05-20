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

            CreateMap<EmployeeCharge, EmployeeChargeViewModel>();
            CreateMap<Employee, EmployeeViewModel>();

            CreateMap<EmployeeContract, EmployeeContractModel>();
            CreateMap<EmployeeContractModel, EmployeeContract>();
        }
    }
}
