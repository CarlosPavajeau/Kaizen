using System.Net;

namespace Kaizen.Infrastructure.Services.Configuration
{
    public class MailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public NetworkCredential Credential { get; set; }
    }
}
