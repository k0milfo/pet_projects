namespace Pingo.Messages.Service.Interfaces;

public interface IMessageDataRepository<T>
{
    public Task<T?> GetAsync(Guid id);

    public Task<IList<T>> GetAllAsync();

    public Task<bool> DeleteAsync(Guid id);

    public Task<bool> InsertAsync(T entity);

    public Task<bool> UpdateAsync(T entity, T entity1);
}
