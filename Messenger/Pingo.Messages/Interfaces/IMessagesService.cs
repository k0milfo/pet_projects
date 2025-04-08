using CSharpFunctionalExtensions;
using Pingo.Messages.Entity;

namespace Pingo.Messages.Interfaces;

public interface IMessagesService
{
    Task<Result<IReadOnlyList<Message>>> GetMessagesAsync();

    Task UpsertMessageAsync(Guid id, string message);
}
