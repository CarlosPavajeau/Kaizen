using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Models.Client;

namespace Kaizen.Mappers
{
    public class ClientMapperProfile : Profile
    {
        public ClientMapperProfile()
        {
            CreateMap<ClientEditModel, Client>();
            CreateMap<ClientInputModel, Client>();

            CreateMap<ClientAddressInputModel, ClientAddress>();
            CreateMap<ContactPersonInputModel, ContactPerson>();

            CreateMap<Client, ClientViewModel>();
            CreateMap<ClientAddress, ClientAddressViewModel>();
            CreateMap<ContactPerson, ContactPersonViewModel>();
        }
    }
}
