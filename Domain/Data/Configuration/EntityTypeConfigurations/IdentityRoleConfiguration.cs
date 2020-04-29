using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaizen.Domain.Data.Configuration.EntityTypeConfigurations
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(191);
            builder.Property(p => p.NormalizedName).HasMaxLength(191);
            builder.Property(p => p.Id).HasMaxLength(30);

            builder.HasData(new IdentityRole[]
            {
                new IdentityRole("Administrator"),
                new IdentityRole("OfficeEmployee"),
                new IdentityRole("TechnicalEmployee"),
                new IdentityRole("Client")
            });
        }
    }
}
