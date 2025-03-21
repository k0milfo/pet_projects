using AutoMapper;
using Pingo.Messages.Entity;

namespace Pingo.Messages.WebApi.Entity;

internal sealed class MappingProfileWebApi : Profile
{
    public MappingProfileWebApi()
    {
        CreateMap<MessageService, MessageResponseWebApi>().ReverseMap();
        CreateMap<MessageWebApi, MessageService>()
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.SentAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
    }
}
