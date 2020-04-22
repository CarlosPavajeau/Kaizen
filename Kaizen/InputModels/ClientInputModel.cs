using Kaizen.EditModels;
using System.Collections.Generic;

namespace Kaizen.InputModels
{
    public class ClientInputModel : ClientEditModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public ClientAddressInputModel ClientAddress { get; set; }
        public List<ContactPersonInputModel> ContactPeople { get; set; }
    }
}
