using Index = FrontendMessage.Pages.Index;

namespace Pingo.Messages.Service.Interfaces;

public interface IMessageDataRepository<T>
{
    Task<T?> GetAsync(Guid id);

    Task<IReadOnlyList<T>> GetAllAsync();

    Task<bool> DeleteAsync(Guid id);

    Task<bool> InsertAsync(Index.MessageFrontend entity);

    Task UpdateAsync(Index.MessageFrontend entity);
}
