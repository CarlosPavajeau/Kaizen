namespace Kaizen.Models.ApplicationUser
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
