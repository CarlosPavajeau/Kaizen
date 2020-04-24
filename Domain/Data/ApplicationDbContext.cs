using Kaizen.Domain.Entities;
using Microsoft.AspNetCore.Identity;
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

            builder.Entity<ApplicationUser>(u =>
            {
                u.Property(p => p.Id).HasMaxLength(191);
                u.Property(p => p.UserName).HasMaxLength(15);
                u.Property(p => p.NormalizedUserName).HasMaxLength(15);
                u.Property(p => p.PasswordHash).HasMaxLength(191);
                u.Property(p => p.PhoneNumber).HasMaxLength(10);
                u.Property(p => p.Email).HasMaxLength(150);
                u.Property(p => p.NormalizedEmail).HasMaxLength(150);
            });

            builder.Entity<IdentityUserLogin<string>>(l =>
            {
                l.Property(p => p.LoginProvider).HasMaxLength(128);
                l.Property(p => p.ProviderKey).HasMaxLength(128);
            });

            builder.Entity<IdentityUserToken<string>>(t =>
            {
                t.Property(p => p.LoginProvider).HasMaxLength(128);
                t.Property(p => p.Name).HasMaxLength(128);
            });

            builder.Entity<IdentityRole>(r =>
            {
                r.Property(p => p.Name).HasMaxLength(191);
                r.Property(p => p.NormalizedName).HasMaxLength(191);
                r.Property(p => p.Id).HasMaxLength(30);
            });

            builder.Entity<Client>(c =>
            {
                c.HasIndex(i => i.NIT)
                    .IsUnique();
                c.Property("UserId").HasMaxLength(191);
            });
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ContactPerson> ContactPeople { get; set; }
        public DbSet<ClientAddress> ClientAddresses { get; set; }
    }
}
