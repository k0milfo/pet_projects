using Pingo.Messages.Entity;

namespace Pingo.Messages.Service.Interfaces;

public interface IMessageDataRepository
{
    Task<MessageService?> GetAsync(Guid id);

    Task<IReadOnlyList<MessageService>> GetAllAsync();

    Task<bool> DeleteAsync(Guid id);

    Task InsertAsync(MessageService messagesEntity);

    Task UpdateAsync(MessageService messagesEntity);
}
