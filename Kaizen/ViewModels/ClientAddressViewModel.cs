using Kaizen.Domain.Entities;
using Kaizen.InputModels;

namespace Kaizen.ViewModels
{
    public class ClientAddressViewModel : ClientAddressInputModel
    {
        public ClientAddressViewModel(ClientAddress clientAddress)
        {
            City = clientAddress.City;
            Neighborhood = clientAddress.Neighborhood;
            Street = clientAddress.Street;
        }
    }
}
