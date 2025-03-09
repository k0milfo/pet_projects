using Microsoft.AspNetCore.Mvc;
using Pingo.Identity.WebApi.Entity;

namespace Pingo.Identity.WebApi.Service.Interface;

public interface IIdentityService<in T>
{
    Task InsertAsync(T request);

    Task<bool> GetAsync(LoginRequest request);

    Task<TokenResponse> LoginAsync(LoginRequest request);

    Task<TokenResponse> RefreshAsync(RefreshTokenRequest request);
}
