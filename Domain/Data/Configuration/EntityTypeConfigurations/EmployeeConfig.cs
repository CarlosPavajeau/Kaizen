using Kaizen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaizen.Domain.Data.Configuration.EntityTypeConfigurations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasIndex(e => e.ContractCode).IsUnique();
            builder.Property("UserId").HasMaxLength(191);
            builder.HasIndex(p => p.UserId).IsUnique();
            builder.Property(e => e.State).HasDefaultValue(EmployeeState.Active);
        }
    }
}
