using Kaizen.Core.Services;
using System;
using System.Threading.Tasks;

namespace Kaizen.Infrastructure.Services
{
    public class MailService : IMailService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            throw new NotImplementedException();
        }
    }
}
