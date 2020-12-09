using Kaizen.Models.ApplicationUser;

namespace Kaizen.Models.Client
{
    public class ClientViewModel : ClientEditModel
    {
        public string Id { get; set; }
        public ApplicationUserViewModel User { get; set; }
    }
}
