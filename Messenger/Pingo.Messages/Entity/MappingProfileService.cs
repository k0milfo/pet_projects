using AutoMapper;

namespace Pingo.Messages.Entity;

internal sealed class MappingProfileService : Profile
{
    public MappingProfileService()
    {
        CreateMap<MessageService, MessageRepository>().ReverseMap();
    }
}
