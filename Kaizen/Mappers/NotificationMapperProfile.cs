using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Models.Notification;

namespace Kaizen.Mappers
{
    public class NotificationMapperProfile : Profile
    {
        public NotificationMapperProfile()
        {
            CreateMap<Notification, NotificationViewModel>();
            CreateMap<NotificationEditModel, Notification>();
        }
    }
}
