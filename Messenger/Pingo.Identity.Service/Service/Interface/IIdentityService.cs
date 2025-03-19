using CSharpFunctionalExtensions;
using Pingo.Identity.Service.Entity;

namespace Pingo.Identity.Service.Service.Interface;

public interface IIdentityService
{
    Task InsertAsync(RegisterRequest request);

    Task<Result<TokenResponse>> LoginAsync(LoginRequest request);

    Task<Result<TokenResponse>> RefreshAsync(RefreshTokenRequest request);
}
