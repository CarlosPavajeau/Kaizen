using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Models.ApplicationUser;

namespace Kaizen.Mappers
{
    public class ApplicationUserMapperProfile : Profile
    {
        public ApplicationUserMapperProfile()
        {
            CreateMap<ApplicationUserInputModel, ApplicationUser>();
            CreateMap<ApplicationUser, ApplicationUserViewModel>();
        }
    }
}
