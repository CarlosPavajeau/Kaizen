using Kaizen.Domain.Entities;
using Kaizen.EditModels;

namespace Kaizen.ViewModels
{
    public class ClientViewModel : ClientEditModel
    {
        public ClientViewModel()
        {

        }
        public string Id { get; set; }
    }
}
