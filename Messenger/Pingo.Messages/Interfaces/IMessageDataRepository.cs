using Pingo.Messages.Entity;

namespace Pingo.Messages.Interfaces;

internal interface IMessageDataRepository
{
    Task<Message?> GetAsync(Guid id);

    Task<IReadOnlyList<Message>> GetAllAsync();

    Task<bool> DeleteAsync(Guid id);

    Task InsertAsync(Message messagesEntity);

    Task UpdateAsync(Message messagesEntity);
}
