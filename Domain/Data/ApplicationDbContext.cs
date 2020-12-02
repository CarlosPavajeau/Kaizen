using Kaizen.Domain.Entities;
using Kaizen.Domain.Extensions;
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

            builder.ApplyCustomDbConfigs();
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
        public DbSet<Sector> Sectors { get; set; }

        public DbSet<ServiceInvoice> ServiceInvoices { get; set; }
        public DbSet<ServiceInvoiceDetail> ServiceInvoiceDetails { get; set; }
        public DbSet<ProductInvoice> ProductInvoices { get; set; }
        public DbSet<ProductInvoiceDetail> ProductInvoiceDetails { get; set; }

        public DbSet<DayStatistics> DayStatistics { get; set; }
        public DbSet<MonthStatistics> MonthStatistics { get; set; }
        public DbSet<YearStatistics> YearStatistics { get; set; }
    }
}
