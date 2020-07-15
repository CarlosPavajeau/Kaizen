using Kaizen.Core.Services;
using Microsoft.Extensions.Hosting;

namespace Kaizen.Infrastructure.Services.MailTemplates
{
    public class ContractCloseToExpirationMailTemplate : MailTemplate, IMailTemplate<ContractCloseToExpirationMailTemplate>
    {
        public ContractCloseToExpirationMailTemplate(IHostEnvironment hostEnvironment) : base(hostEnvironment, "ContractCloseToExpiration.html", 3)
        {
        }
    }
}
