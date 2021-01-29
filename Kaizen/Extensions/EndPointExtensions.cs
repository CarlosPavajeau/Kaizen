using Kaizen.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Kaizen.Extensions
{
    public static class EndPointExtensions
    {
        public static void ConfigureHubMaps(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHub<ServiceRequestHub>("/ServiceRequestsHub");
            endpoints.MapHub<ClientHub>("/ClientsHub");
            endpoints.MapHub<ActivityHub>("/ActivitiesHub");
            endpoints.MapHub<InvoiceHub>("/InvoicesHub");
        }
    }
}
