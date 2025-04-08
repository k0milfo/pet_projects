using AutoMapper;
using CSharpFunctionalExtensions;
using Pingo.Messages.Entity;
using Pingo.Messages.Interfaces;

namespace Pingo.Messages.Repositories;

internal sealed class MessagesService(IMessageDataRepository repository, IMapper mapper, TimeProvider timeProvider) : IMessagesService
{
    public async Task<Result<IReadOnlyList<Message>>> GetMessagesAsync()
    {
        var messages = await repository.GetAllAsync();

        return Result.Success(mapper.Map<IReadOnlyList<Message>>(messages.Value));
    }

    public async Task UpsertMessageAsync(Guid id, string message)
    {
        var oldMessage = await repository.GetAsync(id);

        if (oldMessage.Value == null)
        {
            await repository.InsertAsync(new Message(id, message, timeProvider.GetUtcNow(), UpdatedAt: null));
        }
        else
        {
            oldMessage = oldMessage.Value! with
            {
                UpdatedAt = timeProvider.GetUtcNow(),
            };
            await repository.UpdateAsync(oldMessage.Value!);
        }
    }
}
