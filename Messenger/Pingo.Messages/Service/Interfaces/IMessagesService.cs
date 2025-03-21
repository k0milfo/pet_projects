using Pingo.Messages.Entity;

namespace Pingo.Messages.Service.Interfaces;

public interface IMessagesService
{
    Task<IReadOnlyList<MessageService>> GetMessagesAsync();

    Task UpsertMessageAsync(Guid id, MessageService entity);
}
