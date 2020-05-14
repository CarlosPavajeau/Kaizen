using Kaizen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaizen.Domain.Data.Configuration.EntityTypeConfigurations
{
    public class ActivityServiceConfig : IEntityTypeConfiguration<ActivityService>
    {
        public void Configure(EntityTypeBuilder<ActivityService> builder)
        {
            builder.HasKey(x => new { x.ActivityCode, x.ServiceCode });
        }
    }
}
