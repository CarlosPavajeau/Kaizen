using System;
using System.IO;
using System.Text;
using Kaizen.Core.Services;
using Microsoft.Extensions.Hosting;

namespace Kaizen.Infrastructure.Services.MailTemplates
{
    public class MailTemplate : IMailTemplate
    {
        private readonly IHostEnvironment _hostEnvironment;
        public MailTemplate(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public string LoadTemplate(string templateName, params string[] args)
        {
            string templateFolder = Path.Combine(_hostEnvironment.ContentRootPath, IMailTemplate.EMAIL_TEMPLATES_FOLDER);
            string file = Path.Combine(templateFolder, templateName);

            if (!File.Exists(file))
                throw new ArgumentException($"Email template {templateName} does not exists");

            StringBuilder emailTemplate = new StringBuilder();

            using FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            using StreamReader streamReader = new StreamReader(fileStream);

            emailTemplate.Append(streamReader.ReadToEnd());

            int index = 0;
            foreach (var item in args)
                emailTemplate.Replace($"{{{++index}}}", item);


            return emailTemplate.ToString();
        }
    }
}
