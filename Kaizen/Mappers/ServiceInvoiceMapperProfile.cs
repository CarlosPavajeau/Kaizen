using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Models.ServiceInvoice;

namespace Kaizen.Mappers
{
    public class ServiceInvoiceMapperProfile : Profile
    {
        public ServiceInvoiceMapperProfile()
        {
            CreateMap<ServiceInvoiceDetail, ServiceInvoiceDetailViewModel>();

            CreateMap<ServiceInvoiceEditModel, ServiceInvoice>();
            CreateMap<ServiceInvoice, ServiceInvoiceViewModel>();
        }
    }
}
