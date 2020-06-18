using Kaizen.Core.Services;
using Microsoft.Extensions.Hosting;

namespace Kaizen.Infrastructure.Services.MailTemplates
{
    public class ClientMailTemplate : MailTemplate, IMailTemplate<ClientMailTemplate>
    {
        public ClientMailTemplate(IHostEnvironment hostEnvironment) : base(hostEnvironment, "NewClient.html", 6)
        {
        }
    }
}
