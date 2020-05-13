using Kaizen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaizen.Domain.Data.Configuration.EntityTypeConfigurations
{
    public class ProductServiceConfig : IEntityTypeConfiguration<ProductService>
    {
        public void Configure(EntityTypeBuilder<ProductService> builder)
        {
            builder.HasKey(x => new { x.ServiceCode, x.ProductCode });
        }
    }
}
