using CSharpFunctionalExtensions;
using Pingo.Identity.Service.Entity.Requests;
using Pingo.Identity.Service.Entity.Responses;

namespace Pingo.Identity.Service.Interface;

public interface IIdentityService
{
    Task<Result> InsertAsync(RegisterRequest request);

    Task<Result<TokenResponse>> LoginAsync(LoginRequest request);

    Task<Result<TokenResponse>> RefreshAsync(RefreshTokenRequest request);
}
