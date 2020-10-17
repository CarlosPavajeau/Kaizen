using Kaizen.Domain.Data.Configuration.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Domain.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyCustomDbConfigs(this ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new IdentityUserLoginConfiguration());
            builder.ApplyConfiguration(new IdentityUserTokenConfiguration());
            builder.ApplyConfiguration(new IdentityRoleConfiguration());
            builder.ApplyConfiguration(new ClientConfiguration());
            builder.ApplyConfiguration(new ClientAddressConfig());
            builder.ApplyConfiguration(new EmployeeChargeConfiguration());
            builder.ApplyConfiguration(new EmployeeConfig());
            builder.ApplyConfiguration(new ServiceTypesConfiguration());
            builder.ApplyConfiguration(new ProductServiceConfig());
            builder.ApplyConfiguration(new EmployeeServiceConfig());
            builder.ApplyConfiguration(new EquipmentServiceConfig());
            builder.ApplyConfiguration(new ServiceRequestConfig());
            builder.ApplyConfiguration(new ServiceRequestServiceConfig());
            builder.ApplyConfiguration(new ActivityConfig());
            builder.ApplyConfiguration(new ActivityEmployeeConfig());
            builder.ApplyConfiguration(new ActivityServiceConfig());
            builder.ApplyConfiguration(new WorkOrderConfig());
            builder.ApplyConfiguration(new SectorConfig());
        }
    }
}
