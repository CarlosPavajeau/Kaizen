using Kaizen.Models.InputModels;

namespace Kaizen.Models.ViewModels
{
    public class ApplicationUserViewModel : ApplicationUserInputModel
    {
        public ApplicationUserViewModel(ApplicationUser user)
        {
            Id = user.Id;
            Username = user.UserName;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            Password = null;
        }

        public string Token { get; set; }
        public string Id { get; set; }
    }
}
