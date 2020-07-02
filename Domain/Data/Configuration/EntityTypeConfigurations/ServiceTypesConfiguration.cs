using Kaizen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaizen.Domain.Data.Configuration.EntityTypeConfigurations
{
    public class ServiceTypesConfiguration : IEntityTypeConfiguration<ServiceType>
    {
        public void Configure(EntityTypeBuilder<ServiceType> builder)
        {
            builder.HasData(new ServiceType[]
            {
                new ServiceType { Id = 1, Name = "Control de plagas" },
                new ServiceType { Id = 2, Name = "Desinfección de ambientes y superficies" },
                new ServiceType { Id = 3, Name = "Captura y reubicación de animales" },
                new ServiceType { Id = 4, Name = "Matenimiento de sistemas y equipos" },
                new ServiceType { Id = 5, Name = "Jardinería" },
                new ServiceType { Id = 6, Name = "Suministro, instalación y mantenimiento de equipos" }
            });
        }
    }
}
