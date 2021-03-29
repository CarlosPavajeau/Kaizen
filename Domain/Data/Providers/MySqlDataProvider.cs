using System;
using Kaizen.Domain.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Kaizen.Domain.Data.Providers
{
    public class MySqlDataProvider : IDataProvider
    {
        public DataProvider Provider { get; } = DataProvider.MySql;

        public ApplicationDbContext CreateDbContext(string connectionString)
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 20)),
                mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            return new ApplicationDbContext(optionBuilder.Options);
        }

        public IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 20)),
                mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            });

            return services;
        }
    }
}
