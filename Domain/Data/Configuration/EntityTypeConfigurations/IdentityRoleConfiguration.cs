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
            builder.Property(p => p.Id).HasMaxLength(50);

            builder.HasData(new IdentityRole[]
            {
                new IdentityRole
                {
                    Id = "3bb4b79d-85a4-4a94-b55e-5619c9acf4a2",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    ConcurrencyStamp = "1ed77447-fe5c-42c2-9711-3f91cc103255"
                },
                new IdentityRole
                {
                    Id = "e88f6181-e86a-49e1-a2da-c79c71914624",
                    Name = "OfficeEmployee",
                    NormalizedName = "OFFICEEMPLOYEE",
                    ConcurrencyStamp = "177cda8b-1541-411e-8891-62f58b0e45fa"
                },
                new IdentityRole
                {
                    Id = "e6728857-7423-443f-8228-2c8dd22f3aab",
                    Name = "TechnicalEmployee",
                    NormalizedName = "TECHNICALEMPLOYEE",
                    ConcurrencyStamp = "501614ae-a5ad-4ee3-ba6f-17c28ab1cd5d"
                },
                new IdentityRole
                {
                    Id = "a988a9ea-c7a5-4329-aceb-3da5016c6a43",
                    Name = "Client",
                    NormalizedName = "CLIENT",
                    ConcurrencyStamp = "fba45aab-42d7-4e12-9dc0-44a2f68badf1"
                }
            });
        }
    }
}
