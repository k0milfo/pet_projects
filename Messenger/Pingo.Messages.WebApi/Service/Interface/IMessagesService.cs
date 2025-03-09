namespace Pingo.Messages.WebApi.Service.Interface;

public interface IMessagesService<T>
{
    Task<IReadOnlyList<T>> GetMessagesAsync();

    Task UpsertMessageAsync(Guid id, T entity);
}
