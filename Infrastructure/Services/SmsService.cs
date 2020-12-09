using System;
using System.Threading.Tasks;
using Kaizen.Core.Services;

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
