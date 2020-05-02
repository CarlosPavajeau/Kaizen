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
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ContactPerson> ContactPeople { get; set; }
        public DbSet<ClientAddress> ClientAddresses { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeCharge> EmployeeCharges { get; set; }

		public DbSet<Equipment> Equipments { get; set; }
		public DbSet<Product> Products { get; set; }
	}
}
