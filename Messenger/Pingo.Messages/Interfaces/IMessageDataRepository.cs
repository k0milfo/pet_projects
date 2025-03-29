using CSharpFunctionalExtensions;
using Pingo.Messages.Entity;

namespace Pingo.Messages.Interfaces;

internal interface IMessageDataRepository
{
    Task<Result<MessageService?>> GetAsync(Guid id);

    Task<Result<IReadOnlyList<MessageService>>> GetAllAsync();

    Task<Result<bool>> DeleteAsync(Guid id);

    Task InsertAsync(MessageService messagesEntity);

    Task UpdateAsync(MessageService messagesEntity);
}
