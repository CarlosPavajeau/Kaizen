using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Models.ProductInvoice;

namespace Kaizen.Mappers
{
    public class ProductInvoiceMapperProfile : Profile
    {
        public ProductInvoiceMapperProfile()
        {
            CreateMap<ProductInvoiceDetailInputModel, ProductInvoiceDetail>();
            CreateMap<ProductInvoiceDetail, ProductInvoiceDetailViewModel>();

            CreateMap<ProductInvoiceEditModel, ProductInvoice>();
            CreateMap<ProductInvoiceInputModel, ProductInvoice>();
            CreateMap<ProductInvoice, ProductInvoiceViewModel>();
        }
    }
}
