using DataTransferObject;

namespace Pingo.Identity.Service.Service.Interface;

public interface IIdentityRepository<in T>
{
    Task InsertAsync(T entity);

    Task<EntityDto?> GetAsync(string email);
}
