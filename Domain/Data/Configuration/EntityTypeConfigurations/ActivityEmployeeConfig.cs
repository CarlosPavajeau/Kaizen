using Kaizen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaizen.Domain.Data.Configuration.EntityTypeConfigurations
{
    public class ActivityEmployeeConfig : IEntityTypeConfiguration<ActivityEmployee>
    {
        public void Configure(EntityTypeBuilder<ActivityEmployee> builder)
        {
            builder.HasKey(x => new { x.EmployeeId, x.ActivityCode });
        }
    }
}
