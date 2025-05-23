using AutoMapper;
using Pingo.Messages.Entity;
using Pingo.Messages.Interfaces;
using IMessageDataRepository = Pingo.Messages.Interfaces.IMessageDataRepository;

namespace Pingo.Messages.Implementations;

internal sealed class MessagesService(
    IMessageDataRepository repository, IMapper mapper, TimeProvider timeProvider) : IMessagesService
{
    public async Task<IReadOnlyList<Message>> GetMessagesAsync()
    {
        var messages = await repository.GetAllAsync();

        return mapper.Map<IReadOnlyList<Message>>(messages);
    }

    public async Task UpsertMessageAsync(Guid id, string message)
    {
        var oldMessage = await repository.GetAsync(id);

        if (oldMessage == null)
        {
            await repository.InsertAsync(new Message(id, message, timeProvider.GetUtcNow(), UpdatedAt: null));
        }
        else
        {
            oldMessage = oldMessage! with
            {
                UpdatedAt = timeProvider.GetUtcNow(),
            };
            await repository.UpdateAsync(oldMessage);
        }
    }

    public async Task SendWelcomeMessageAsync(Guid id, string message)
    {
        await repository.InsertAsync(new Message(id, message, timeProvider.GetUtcNow(), UpdatedAt: null));
    }
}
