using Pingo.Messages.Entity;

namespace Pingo.Messages.Interfaces;

public interface IMessagesService
{
    Task<IReadOnlyList<Message>> GetMessagesAsync();

    Task UpsertMessageAsync(Guid id, string message);

    Task SendWelcomeMessageAsync(Guid id, string message);
}
