using Kaizen.Domain.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Kaizen.Domain.Data.Providers
{
    public class SqlServerDataProvider : IDataProvider
    {
        public DataProvider Provider { get; } = DataProvider.SqlServer;

        public ApplicationDbContext CreateDbContext(string connectionString)
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }

        public IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
