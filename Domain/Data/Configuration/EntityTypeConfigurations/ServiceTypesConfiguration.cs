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
                new ServiceType { Id = 2, Name = "Saneamiento" },
                new ServiceType { Id = 3, Name = "Limpieza de espacios" },
                new ServiceType { Id = 4, Name = "Lavado y desinfección de tanques de agua" },
                new ServiceType { Id = 5, Name = "Captura y rehabilidación de animales domesticos" },
                new ServiceType { Id = 6, Name = "Jardineria" }
            });
        }
    }
}
