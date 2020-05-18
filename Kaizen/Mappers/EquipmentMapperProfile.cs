using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Models.Equipment;

namespace Kaizen.Mappers
{
    public class EquipmentMapperProfile : Profile
    {
        public EquipmentMapperProfile()
        {
            CreateMap<EquipmentEditModel, Equipment>();
            CreateMap<EquipmentInputModel, Equipment>();
            CreateMap<Equipment, EquipmentViewModel>();
        }
    }
}
