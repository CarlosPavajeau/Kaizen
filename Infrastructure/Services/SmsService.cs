using Kaizen.Core.Services;
using System;
using System.Threading.Tasks;

namespace Kaizen.Infrastructure.Services
{
    public class SmsService : ISmsService
    {
        public Task SendSmsAsync(string number, string message)
        {
            throw new NotImplementedException();
        }
    }
}
