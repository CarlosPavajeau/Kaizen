using System.Threading.Tasks;

namespace Kaizen.Core.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(string email, string subject, string message, bool isHtml = false);
    }
}
