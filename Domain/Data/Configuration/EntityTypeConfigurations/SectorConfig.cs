using Kaizen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaizen.Domain.Data.Configuration.EntityTypeConfigurations
{
    public class SectorConfig : IEntityTypeConfiguration<Sector>
    {
        public void Configure(EntityTypeBuilder<Sector> builder)
        {
            builder.HasData(new Sector[]
            {
                new Sector { Id = 1, Name = "Industrial" },
                new Sector { Id = 2, Name = "Comercial" },
                new Sector { Id = 3, Name = "Alimentos" },
                new Sector { Id = 4, Name = "Portuario" },
                new Sector { Id = 5, Name = "Hotelero" },
                new Sector { Id = 6, Name = "Salud" },
                new Sector { Id = 7, Name = "Residencial" },
                new Sector { Id = 8, Name = "Educativo" },
                new Sector { Id = 9, Name = "Transporte" }
            });
        }
    }
}
