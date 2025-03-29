using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Pingo.Identity.Service.Entity.Requests;
using Pingo.Identity.Service.Interface;

namespace Pingo.Identity.ComponentTests;

[Collection(DatabaseCollection.NonParallelTests)]
public sealed class ServiceTests : IClassFixture<WebAppFactoryFixture>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly IServiceScope _scope;
    private readonly IIdentityRepository _repository;

    public ServiceTests(WebAppFactoryFixture fixture)
    {
        _client = fixture.CreateClient();
        _scope = fixture.Factory.Services.CreateScope();
        _repository = _scope.ServiceProvider.GetRequiredService<IIdentityRepository>();
    }

    [Fact]
    public async Task UserRegistrationSuccessful()
    {
        var request = new RegisterRequest("string", "string");
        var response = await _client.PostAsJsonAsync("api/identity/register", request);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task ErrorRegistering_ExistingEmail()
    {
        var request = new RegisterRequest("string", "string");
        await _client.PostAsJsonAsync("api/identity/register", request);
        var response = await _client.PostAsJsonAsync("api/identity/register", request);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        problemDetails!.Detail.Should().Be("Пользователь с таким email уже существует.");
    }

    [Fact]
    public async Task SuccessfulLogin()
    {
        var request = new LoginRequest("string", "string");
        await _client.PostAsJsonAsync("api/identity/register", request);
        var response = await _client.PostAsJsonAsync("api/identity/login", request);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task LoginError()
    {
        var request = new LoginRequest("string", "string");
        var response = await _client.PostAsJsonAsync("api/identity/login", request);
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        problemDetails!.Detail.Should().Be("Пользователь с таким email не найден.");

        await _client.PostAsJsonAsync("api/identity/register", request);
        request = request with
        {
            Password = "string1",
        };
        response = await _client.PostAsJsonAsync("api/identity/login", request);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        problemDetails!.Detail.Should().Be("Неверный пароль.");
    }

    [Fact]
    public async Task SuccessfulTokenRefresh()
    {
        var request = new RegisterRequest("string", "string");
        var response = await _client.PostAsJsonAsync("api/identity/register", request);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        response = await _client.PostAsJsonAsync("api/identity/login", request);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var user = await _repository.GetUserAsync(request.Email!);
        var tokenRequest = new RefreshTokenRequest(user.Value!.Email, user.Value.Token);
        response = await _client.PostAsJsonAsync("api/identity/refresh", tokenRequest);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task TokenRefreshError()
    {
        var request = new RegisterRequest("string", "string");
        var response = await _client.PostAsJsonAsync("api/identity/register", request);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        response = await _client.PostAsJsonAsync("api/identity/login", request);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var user = await _repository.GetUserAsync(request.Email!);
        await Task.Delay(1000);
        response = await _client.PostAsJsonAsync("api/identity/login", request);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var tokenRequest = new RefreshTokenRequest(user.Value!.Email, user.Value.Token);
        response = await _client.PostAsJsonAsync("api/identity/refresh", tokenRequest);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        problemDetails!.Detail.Should().Be("Refresh-токен недействителен или истёк.");
    }

    async Task IAsyncLifetime.InitializeAsync() => await WebAppFactoryFixture.ResetAsync();

    Task IAsyncLifetime.DisposeAsync() => Task.CompletedTask;
}
