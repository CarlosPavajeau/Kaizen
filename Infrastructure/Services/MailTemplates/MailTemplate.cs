using System;
using System.IO;
using System.Text;
using Kaizen.Core.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Kaizen.Infrastructure.Services.MailTemplates
{
    public class MailTemplate : IMailTemplate
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ILogger _logger;
        public MailTemplate(IHostEnvironment hostEnvironment, ILogger<MailTemplate> logger)
        {
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        public string LoadTemplate(string templateName, params string[] args)
        {
            string templateFolder = Path.Combine(_hostEnvironment.ContentRootPath, IMailTemplate.EMAIL_TEMPLATES_FOLDER);
            string file = Path.Combine(templateFolder, templateName);

            if (!File.Exists(file))
            {
                throw new ArgumentException($"Email template {templateName} does not exists.");
            }

            StringBuilder emailTemplate = new StringBuilder();
            try
            {
                FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                using StreamReader streamReader = new StreamReader(fileStream);

                emailTemplate.Append(streamReader.ReadToEnd());

                int index = 0;
                foreach (string item in args)
                {
                    emailTemplate.Replace($"{{{++index}}}", item);
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return emailTemplate.ToString();
        }
    }
}
