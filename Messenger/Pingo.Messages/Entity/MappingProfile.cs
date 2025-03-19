using AutoMapper;
using Pingo.Messages.DataTransferObject;

namespace Pingo.Messages.Entity;

internal sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MessagesEntityDto, Message>()
            .ForMember(dest => dest.SentAt, opt => opt.MapFrom(src => src.SentAt ?? DateTimeOffset.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.SentAt != null ? DateTimeOffset.UtcNow : (DateTimeOffset?)null)).ReverseMap();
    }
}
