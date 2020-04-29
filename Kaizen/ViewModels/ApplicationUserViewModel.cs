using Kaizen.InputModels;

namespace Kaizen.ViewModels
{
    public class ApplicationUserViewModel : ApplicationUserInputModel
    {
        public ApplicationUserViewModel()
        {
        }

        public string Token { get; set; }
        public string Id { get; set; }
    }
}
