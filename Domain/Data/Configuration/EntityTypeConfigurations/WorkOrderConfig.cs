using Kaizen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaizen.Domain.Data.Configuration.EntityTypeConfigurations
{
    public class WorkOrderConfig : IEntityTypeConfiguration<WorkOrder>
    {
        public void Configure(EntityTypeBuilder<WorkOrder> builder)
        {
            builder.ToTable("WorkOrders");
            builder.Property(p => p.WorkOrderState).HasDefaultValue(WorkOrderState.Generated);
            builder.HasAlternateKey(p => p.ActivityCode);
        }
    }
}
