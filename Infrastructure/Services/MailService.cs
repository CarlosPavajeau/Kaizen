using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Kaizen.Core.Services;
using Kaizen.Infrastructure.Services.Configuration;
using Microsoft.Extensions.Options;

namespace Kaizen.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly SmtpClient _client = new SmtpClient();

        private MailMessage MailMessage { get; set; }

        public MailService(IOptions<MailSettings> options)
        {
            _mailSettings = options.Value;
            ConfigSMTPClient();
        }

        private void ConfigSMTPClient()
        {
            _client.Host = _mailSettings.Host;
            _client.Port = _mailSettings.Port;
            _client.UseDefaultCredentials = _mailSettings.UseDefaultCredentials;
            _client.Credentials = new NetworkCredential(_mailSettings.Credential.UserName, _mailSettings.Credential.Password);
            _client.EnableSsl = _mailSettings.EnableSsl;
            _client.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        public async Task SendEmailAsync(string email, string subject, string message, bool isHtml = false)
        {
            ConfigEmail(email, subject, message, isHtml);
            await _client.SendMailAsync(MailMessage);
        }

        private void ConfigEmail(string email, string subject, string message, bool isHtml = false)
        {
            MailMessage = new MailMessage
            {
                From = new MailAddress(_mailSettings.Credential.UserName),
                Subject = subject,
                Body = message,
                IsBodyHtml = isHtml,
                Priority = MailPriority.High
            };

            MailMessage.To.Add(email);
        }


    }
}
