using Kaizen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaizen.Domain.Data.Configuration.EntityTypeConfigurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasIndex(i => i.NIT)
                    .IsUnique();
            builder.Property("UserId").HasMaxLength(191);
        }
    }
}
