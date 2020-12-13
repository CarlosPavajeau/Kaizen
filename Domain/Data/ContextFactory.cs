using System;
using System.Linq;
using Kaizen.Core.DependencyResolver;
using Kaizen.Domain.Data.Configuration;
using Microsoft.Extensions.Options;

namespace Kaizen.Domain.Data
{
    public class ContextFactory : IContextFactory
    {
        private Configuration.Data DataConfuguration { get; }
        private ConnectionStrings ConnectionStrings { get; }
        private readonly IResolver _resolver;

        public ContextFactory(IOptions<Configuration.Data> dataOptions, IResolver resolver, IOptions<ConnectionStrings> connectionStringOptions)
        {
            DataConfuguration = dataOptions.Value;
            ConnectionStrings = connectionStringOptions.Value;
            _resolver = resolver;
        }
        public ApplicationDbContext Create()
        {
            IDataProvider dataProvider = _resolver.ResolveAll<IDataProvider>()
                .SingleOrDefault(x => x.Provider == DataConfuguration.Provider);

            if (dataProvider == null)
            {
                throw new NullReferenceException("The Data Provider entry in appsettings.json is empty or the one specified has not been found!");
            }

            return dataProvider.CreateDbContext(ConnectionStrings.DefaultConnection);
        }
    }
}
