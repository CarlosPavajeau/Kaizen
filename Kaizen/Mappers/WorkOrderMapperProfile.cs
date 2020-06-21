using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Models.WorkOrder;

namespace Kaizen.Mappers
{
    public class WorkOrderMapperProfile : Profile
    {
        public WorkOrderMapperProfile()
        {
            CreateMap<WorkOrderEditModel, WorkOrder>();
            CreateMap<WorkOrderInputModel, WorkOrder>();
            CreateMap<WorkOrder, WorkOrderViewModel>();
            CreateMap<Sector, SectorViewModel>();
        }
    }
}
