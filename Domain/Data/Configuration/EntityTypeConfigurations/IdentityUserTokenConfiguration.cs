using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaizen.Domain.Data.Configuration.EntityTypeConfigurations
{
    public class IdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
        {
            builder.Property(p => p.LoginProvider).HasMaxLength(128);
            builder.Property(p => p.Name).HasMaxLength(128);
        }
    }
}
