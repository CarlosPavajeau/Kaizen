using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Models.Activity;

namespace Kaizen.Mappers
{
    public class ActivityMapperProfile : Profile
    {
        public ActivityMapperProfile()
        {
            CreateMap<ActivityEditModel, Activity>();
            CreateMap<ActivityInputModel, Activity>();
            CreateMap<Activity, ActivityViewModel>();
        }
    }
}
