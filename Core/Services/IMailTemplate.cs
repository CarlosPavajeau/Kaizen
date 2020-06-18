namespace Kaizen.Core.Services
{
    public interface IMailTemplate<T>
    {
        protected const string EMAIL_TEMPLATES_FOLDER = "EmailTemplates";
        public int RequiredParams { get; }
        public string TemplateName { get; }

        string LoadTemplate(params string[] args);
    }
}
