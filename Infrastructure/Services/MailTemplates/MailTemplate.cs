using System;
using System.IO;
using System.Text;
using Kaizen.Core.Services;
using Microsoft.Extensions.Hosting;

namespace Kaizen.Infrastructure.Services.MailTemplates
{
    public abstract class MailTemplate : IMailTemplate<MailTemplate>
    {
        private readonly IHostEnvironment _hostEnvironment;
        public MailTemplate(IHostEnvironment hostEnvironment, string templateName, int requiredParams)
        {
            _hostEnvironment = hostEnvironment;
            TemplateName = templateName;
            RequiredParams = requiredParams;
        }
        public int RequiredParams { get; }

        public string TemplateName { get; }

        public string LoadTemplate(params string[] args)
        {
            if (args.Length < RequiredParams)
                throw new ArgumentException("The params length is invalid");

            string templateFolder = Path.Combine(_hostEnvironment.ContentRootPath, IMailTemplate<ClientMailTemplate>.EMAIL_TEMPLATES_FOLDER);
            string file = Path.Combine(templateFolder, TemplateName);

            if (!File.Exists(file))
                throw new ArgumentException($"Email template {TemplateName} does not exists");

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
