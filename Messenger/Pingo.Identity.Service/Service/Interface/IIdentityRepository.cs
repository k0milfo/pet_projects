using Pingo.Identity.Service.Entity;

namespace Pingo.Identity.Service.Service.Interface;

internal interface IIdentityRepository
{
    Task InsertAsync(User entity);

    Task<User?> GetAsync(string email);
}
