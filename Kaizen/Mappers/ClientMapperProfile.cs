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

            CreateMap<ClientAddressModel, ClientAddress>();
            CreateMap<ContactPersonModel, ContactPerson>();

            CreateMap<Client, ClientViewModel>();
            CreateMap<ClientAddress, ClientAddressModel>();
            CreateMap<ContactPerson, ContactPersonModel>();
        }
    }
}
