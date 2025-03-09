using AutoMapper;
using Pingo.Messages.Service.Interfaces;
using Pingo.Messages.WebApi.Service.Interface;
using Index = FrontendMessage.Pages.Index;

namespace Pingo.Messages.WebApi.Service.Implementations;

internal sealed class MessagesService(IMessageDataRepository<Index.MessageFrontend> repository, IMapper mapper) : IMessagesService<Index.MessageFrontend>
{
    public async Task<IReadOnlyList<Index.MessageFrontend>> GetMessagesAsync()
    {
        var messages = await repository.GetAllAsync();

        return mapper.Map<IReadOnlyList<Index.MessageFrontend>>(messages);
    }

    public async Task UpsertMessageAsync(Guid id, Index.MessageFrontend messageApi)
    {
        var oldMessage = await repository.GetAsync(id);

        if (oldMessage == null)
        {
            await repository.InsertAsync(messageApi);
        }
        else
        {
            oldMessage = mapper.Map(messageApi, oldMessage);
            await repository.UpdateAsync(oldMessage);
        }
    }
}
