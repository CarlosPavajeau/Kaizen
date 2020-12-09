using Kaizen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaizen.Domain.Data.Configuration.EntityTypeConfigurations
{
    public class EmployeeChargeConfiguration : IEntityTypeConfiguration<EmployeeCharge>
    {
        public void Configure(EntityTypeBuilder<EmployeeCharge> builder)
        {
            builder.HasData(new[]
            {
                new EmployeeCharge("Gerente") { Id = 1 },
                new EmployeeCharge("Coordinador de Calidad y Ambiente") { Id = 2 },
                new EmployeeCharge("Contador") { Id = 3 },
                new EmployeeCharge("Lider SST") { Id = 4 },
                new EmployeeCharge("Auxiliar Administrativa") { Id = 5 },
                new EmployeeCharge("Técnico Operativo") { Id = 6 },
                new EmployeeCharge("Aprendiz") { Id = 7 }
            });
        }
    }
}
