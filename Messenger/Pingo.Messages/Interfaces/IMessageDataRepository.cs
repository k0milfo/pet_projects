using CSharpFunctionalExtensions;
using Pingo.Messages.Entity;

namespace Pingo.Messages.Interfaces;

internal interface IMessageDataRepository
{
    Task<Result<Message?>> GetAsync(Guid id);

    Task<Result<IReadOnlyList<Message>>> GetAllAsync();

    Task<Result<bool>> DeleteAsync(Guid id);

    Task InsertAsync(Message messagesEntity);

    Task UpdateAsync(Message messagesEntity);
}
