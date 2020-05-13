using Kaizen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaizen.Domain.Data.Configuration.EntityTypeConfigurations
{
    public class ServiceRequestServiceConfig : IEntityTypeConfiguration<ServiceRequestService>
    {
        public void Configure(EntityTypeBuilder<ServiceRequestService> builder)
        {
            builder.HasKey(x => new { x.ServiceCode, x.ServiceRequestCode });
        }
    }
}
