namespace Kaizen.Core.Services
{
    public interface IMailTemplate
    {
        protected const string EMAIL_TEMPLATES_FOLDER = "EmailTemplates";

        string LoadTemplate(string templateName, params string[] args);
    }
}
