using Kaizen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaizen.Domain.Data.Configuration.EntityTypeConfigurations
{
    public class EquipmentServiceConfig : IEntityTypeConfiguration<EquipmentService>
    {
        public void Configure(EntityTypeBuilder<EquipmentService> builder)
        {
            builder.HasKey(x => new { x.EquipmentCode, x.ServiceCode });
        }
    }
}
