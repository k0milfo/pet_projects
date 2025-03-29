using System.Net;
using FluentAssertions;
using Pingo.Identity.Service.Entity.Requests;
using Pingo.Messages.ComponentTests;

namespace IdentityComponentTests;

[Collection(DatabaseCollection.NonParallelTests)]
public sealed class ServiceTests(WebAppFactoryFixture fixture) : IClassFixture<WebAppFactoryFixture>, IAsyncLifetime
{
    private readonly HttpClient _client = fixture.CreateClient();

    [Fact]
    public async Task AddOneMessage_ShouldReturnSingleMessage()
    {
        var request = new RegisterRequest("string", "string");
        var response = await _client.PostAsJsonAsync($"api/identity/register", request);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    async Task IAsyncLifetime.InitializeAsync() => await WebAppFactoryFixture.ResetAsync();

    Task IAsyncLifetime.DisposeAsync() => Task.CompletedTask;
}
