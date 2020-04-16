using Kaizen.Domain.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Kaizen.Domain.Data.Providers
{
    public class MySqlDataProvider : IDataProvider
    {
        public DataProvider Provider { get; } = DataProvider.MySQL;

        public ApplicationDbContext CreateDbContext(string connectionString)
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseMySql(connectionString);

            return new ApplicationDbContext(optionBuilder.Options);
        }

        public IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(connectionString);
            });

            return services;
        }
    }
}
