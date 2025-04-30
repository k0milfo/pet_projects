using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Pingo.Identity.Service;
using Pingo.Identity.Service.Entity.Requests;
using Pingo.Identity.Service.Entity.Responses;

namespace Pingo.Identity.ComponentTests;

[Collection(DatabaseCollection.NonParallelTests)]
public sealed class ServiceTests : IClassFixture<WebAppFactoryFixture>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly WebAppFactoryFixture _fixture;

    public ServiceTests(WebAppFactoryFixture fixture)
    {
        _client = fixture.CreateClient();
        _fixture = fixture;
    }

    [Fact]
    public async Task UserRegistrationSuccessful()
    {
        // Arrange
        var request = new RegisterRequest("string", "string");

        // Act
        var response = await _client.PostAsJsonAsync("api/identity/register", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task ErrorRegistering_ExistingEmail()
    {
        // Arrange
        var request = new RegisterRequest("string", "string");

        // Act
        await _client.PostAsJsonAsync("api/identity/register", request);
        var response = await _client.PostAsJsonAsync("api/identity/register", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        problemDetails!.Detail.Should().Be(LoginErrorType.EmailAlreadyExists.ToString());
    }

    [Fact]
    public async Task SuccessfulLogin()
    {
        // Arrange
        var request = new RegisterRequest("string", "string");

        // Act
        await _client.PostAsJsonAsync("api/identity/register", request);
        var response = await _client.PostAsJsonAsync("api/identity/login", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task LoginError()
    {
        // Arrange
        var request = new RegisterRequest("string", "string");

        // Act
        var response = await _client.PostAsJsonAsync("api/identity/login", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        problemDetails!.Detail.Should().Be(LoginErrorType.EmailNotFound.ToString());

        // Act
        await _client.PostAsJsonAsync("api/identity/register", request);

        // Arrange
        request = request with
        {
            Password = "string1",
        };

        // Act
        response = await _client.PostAsJsonAsync("api/identity/login", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        problemDetails!.Detail.Should().Be(LoginErrorType.InvalidPassword.ToString());
    }

    [Fact]
    public async Task SuccessfulTokenRefresh()
    {
        // Arrange
        var request = new RegisterRequest("string", "string");

        // Act
        var response = await _client.PostAsJsonAsync("api/identity/register", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // Act
        response = await _client.PostAsJsonAsync("api/identity/login", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Arrange
        var loginResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
        var tokenRequest = new RefreshTokenRequest(request.Email, loginResponse!.RefreshToken);

        // Act
        response = await _client.PostAsJsonAsync("api/identity/refresh", tokenRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task TokenRefreshError()
    {
        // Arrange
        var request = new RegisterRequest("string", "string");

        // Act
        var response = await _client.PostAsJsonAsync("api/identity/register", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // Act
        response = await _client.PostAsJsonAsync("api/identity/login", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Act
        response = await _client.PostAsJsonAsync("api/identity/login", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Arrange
        var tokenRequest = new RefreshTokenRequest(request.Email, RefreshToken: Guid.NewGuid());

        // Act
        response = await _client.PostAsJsonAsync("api/identity/refresh", tokenRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        problemDetails!.Detail.Should().Be(LoginErrorType.InvalidRefreshToken.ToString());
    }

    public async Task InitializeAsync() => await _fixture.ResetAsync();

    public async Task DisposeAsync() => await Task.CompletedTask;
}
