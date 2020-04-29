using Kaizen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaizen.Domain.Data.Configuration.EntityTypeConfigurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(p => p.Id).HasMaxLength(191);
            builder.Property(p => p.UserName).HasMaxLength(15);
            builder.Property(p => p.NormalizedUserName).HasMaxLength(15);
            builder.Property(p => p.PasswordHash).HasMaxLength(191);
            builder.Property(p => p.PhoneNumber).HasMaxLength(10);
            builder.Property(p => p.Email).HasMaxLength(150);
            builder.Property(p => p.NormalizedEmail).HasMaxLength(150);
        }
    }
}
