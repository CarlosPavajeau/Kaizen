using Kaizen.Domain.Data.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kaizen.Domain.Data
{
    public interface IDataProvider
    {
        DataProvider Provider { get; }

        IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString);
        ApplicationDbContext CreateDbContext(string connectionString);
    }
}
