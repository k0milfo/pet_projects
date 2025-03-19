using Pingo.Messages.DataTransferObject;

namespace Pingo.Messages.Service.Interfaces;

public interface IMessagesService
{
    Task<IReadOnlyList<MessagesEntityDto>> GetMessagesAsync();

    Task UpsertMessageAsync(Guid id, MessagesEntityDto entity);
}
