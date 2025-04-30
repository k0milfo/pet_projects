using CSharpFunctionalExtensions;
using Pingo.Identity.Service.Entity.Requests;
using Pingo.Identity.Service.Entity.Responses;

namespace Pingo.Identity.Service.Interface;

public interface IIdentityService
{
    Task<UnitResult<LoginErrorType>> InsertAsync(RegisterRequest request);

    Task<Result<TokenResponse, LoginErrorType>> LoginAsync(AuthRequest request);

    Task<Result<TokenResponse, LoginErrorType>> RefreshTokenAsync(RefreshTokenRequest request);
}
