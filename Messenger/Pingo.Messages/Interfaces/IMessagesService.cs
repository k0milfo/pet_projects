using CSharpFunctionalExtensions;
using Pingo.Messages.Entity;

namespace Pingo.Messages.Interfaces;

public interface IMessagesService
{
    Task<Result<IReadOnlyList<MessageService>>> GetMessagesAsync();

    Task UpsertMessageAsync(Guid id, MessageService entity);
}
