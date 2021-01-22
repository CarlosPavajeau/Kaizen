namespace Kaizen.Core.Services
{
    public interface IMailTemplate
    {
        protected const string EmailTemplatesFolder = "EmailTemplates";

        string LoadTemplate(string templateName, params string[] args);
    }
}
