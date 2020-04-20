using System.Threading.Tasks;

namespace Kaizen.Core.Services
{
    public interface ISmsService
    {
        Task SendSmsAsync(string number, string message);
    }
}
