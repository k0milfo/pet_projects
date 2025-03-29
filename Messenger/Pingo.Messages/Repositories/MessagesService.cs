using AutoMapper;
using CSharpFunctionalExtensions;
using Pingo.Messages.Entity;
using Pingo.Messages.Interfaces;

namespace Pingo.Messages.Repositories;

internal sealed class MessagesService(IMessageDataRepository repository, IMapper mapper) : IMessagesService
{
    public async Task<Result<IReadOnlyList<MessageService>>> GetMessagesAsync()
    {
        var messages = await repository.GetAllAsync();

        return Result.Success(mapper.Map<IReadOnlyList<MessageService>>(messages.Value));
    }

    public async Task UpsertMessageAsync(Guid id, MessageService entity)
    {
        var oldMessage = await repository.GetAsync(id);

        if (oldMessage.Value == null)
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
            oldMessage = oldMessage.Value! with
            {
                Id = id,
            };
            await repository.UpdateAsync(oldMessage.Value!);
        }
    }
}
