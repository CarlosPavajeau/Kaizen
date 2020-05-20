using Kaizen.Domain.Data.Configuration.EntityTypeConfigurations;
using Kaizen.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Domain.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new IdentityUserLoginConfiguration());
            builder.ApplyConfiguration(new IdentityUserTokenConfiguration());
            builder.ApplyConfiguration(new IdentityRoleConfiguration());
            builder.ApplyConfiguration(new ClientConfiguration());
            builder.ApplyConfiguration(new EmployeeChargeConfiguration());
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
            builder.ApplyConfiguration(new EmployeeConfig());
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ContactPerson> ContactPeople { get; set; }
        public DbSet<ClientAddress> ClientAddresses { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeCharge> EmployeeCharges { get; set; }

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<ProductService> ProductsServices { get; set; }
        public DbSet<EquipmentService> EquipmentsServices { get; set; }
        public DbSet<EmployeeService> EmployeesServices { get; set; }

        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<ServiceRequestService> ServiceRequestsServices { get; set; }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityEmployee> ActivitiesEmployees { get; set; }
        public DbSet<ActivityService> ActivitiesServices { get; set; }

        public DbSet<WorkOrder> WorkOrders { get; set; }
    }
}
