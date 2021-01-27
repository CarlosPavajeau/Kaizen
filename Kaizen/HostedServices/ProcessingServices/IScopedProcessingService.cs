using System.Threading;
using System.Threading.Tasks;

namespace Kaizen.HostedServices.ProcessingServices
{
    internal interface IScopedProcessingService
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}
