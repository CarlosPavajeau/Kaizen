using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Models.Product;

namespace Kaizen.Mappers
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<ProductEditModel, Product>();
            CreateMap<ProductInputModel, Product>();
            CreateMap<Product, ProductViewModel>();
        }
    }
}
