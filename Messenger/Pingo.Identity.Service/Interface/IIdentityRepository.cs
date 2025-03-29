using CSharpFunctionalExtensions;
using Pingo.Identity.Service.Entity.Models;
using Pingo.Identity.Service.Entity.Requests;

namespace Pingo.Identity.Service.Interface;

internal interface IIdentityRepository
{
    Task InsertUserAsync(User entity);

    Task<Result<User?>> GetUserAsync(string email);

    Task InsertRefreshTokenAsync(RefreshTokenRequest request);

    Task DeleteRefreshTokenAsync(RefreshTokenRequest request);
}
