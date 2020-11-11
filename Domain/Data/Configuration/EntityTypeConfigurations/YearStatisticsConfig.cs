using Kaizen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaizen.Domain.Data.Configuration.EntityTypeConfigurations
{
    public class YearStatisticsConfig : IEntityTypeConfiguration<YearStatistics>
    {
        public void Configure(EntityTypeBuilder<YearStatistics> builder)
        {
            builder.HasAlternateKey(p => p.Year);
        }
    }
}
