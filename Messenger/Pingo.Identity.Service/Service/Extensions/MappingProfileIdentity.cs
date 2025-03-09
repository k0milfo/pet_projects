using AutoMapper;
using DataTransferObject;
using Pingo.Identity.Service.Entity;

namespace Pingo.Messages.WebApi.Entity;

public sealed class MappingProfileIdentity : Profile
{
    public MappingProfileIdentity()
    {
        CreateMap<EntityDto, User>();
        CreateMap<User, EntityDto>();
    }
}
