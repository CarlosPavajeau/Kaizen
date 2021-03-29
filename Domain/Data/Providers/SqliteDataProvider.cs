using Kaizen.Domain.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Kaizen.Domain.Data.Providers
{
    public class SqliteDataProvider : IDataProvider
    {
        public DataProvider Provider { get; } = DataProvider.SqLite;

        public ApplicationDbContext CreateDbContext(string connectionString)
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseSqlite(connectionString);

            return new ApplicationDbContext(optionBuilder.Options);
        }

        public IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            return services;
        }
    }
}
