using AutoMapper;
using Pingo.Messages.Entity;
using Pingo.Messages.Service.Interfaces;

namespace Pingo.Messages.Service.Repositories;

internal sealed class MessagesService(IMessageDataRepository repository, IMapper mapper) : IMessagesService
{
    public async Task<IReadOnlyList<MessageService>> GetMessagesAsync()
    {
        var messages = await repository.GetAllAsync();

        return mapper.Map<IReadOnlyList<MessageService>>(messages);
    }

    public async Task UpsertMessageAsync(Guid id, MessageService entity)
    {
        var oldMessage = await repository.GetAsync(id);

        if (oldMessage == null)
        {
            entity = entity with
            {
                Id = id,
            };
            await repository.InsertAsync(entity);
        }
        else
        {
            oldMessage = mapper.Map(entity, oldMessage);
            oldMessage = oldMessage with
            {
                Id = id,
            };
            await repository.UpdateAsync(oldMessage);
        }
    }
}
