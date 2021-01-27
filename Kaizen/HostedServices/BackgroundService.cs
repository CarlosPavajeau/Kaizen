using System;

namespace Kaizen.HostedServices
{
    public abstract class BackgroundService : Microsoft.Extensions.Hosting.BackgroundService
    {
        protected IServiceProvider ServiceProvider { get; set; }

        protected BackgroundService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
