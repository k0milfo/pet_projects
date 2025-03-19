using Pingo.Messages.DataTransferObject;

namespace Pingo.Messages.Service.Interfaces;

public interface IMessageDataRepository
{
    Task<MessagesEntityDto?> GetAsync(Guid id);

    Task<IReadOnlyList<MessagesEntityDto>> GetAllAsync();

    Task<bool> DeleteAsync(Guid id);

    Task InsertAsync(MessagesEntityDto messagesEntity);

    Task UpdateAsync(MessagesEntityDto messagesEntity);
}
