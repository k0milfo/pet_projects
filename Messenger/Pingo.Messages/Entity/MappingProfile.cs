using AutoMapper;
using Pingo.Messages.Entity;
using Index = FrontendMessage.Pages.Index;

namespace Pingo.Messages.WebApi.Entity;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Index.MessageFrontend, Message>();
        CreateMap<Message, Index.MessageFrontend>();
    }
}
