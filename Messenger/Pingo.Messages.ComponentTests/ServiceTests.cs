using FluentAssertions;
using Index = FrontendMessage.Pages.Index;

namespace Pingo.Messages.ComponentTests;

[Collection(DatabaseCollection.NonParallelTests)]
public sealed class ServiceTests(WebAppFactoryFixture fixture) : IClassFixture<WebAppFactoryFixture>, IAsyncLifetime
{
    private readonly HttpClient _client = fixture.CreateClient();

    [Fact]
    public async Task GetAllMessages_ShouldReturnEmptyList()
    {
        var content = await ListMessagesAsync();
        content.Should().BeEmpty();
    }

    [Fact]
    public async Task AddOneMessage_ShouldReturnSingleMessage()
    {
        var id = Guid.NewGuid();
        var message = new Index.MessageFrontend(id, "Hello, world!", TimeProvider.System.GetUtcNow(), UpdatedAt: null);

        await _client.PutAsJsonAsync($"api/messages/{id}", message);
        var content = await ListMessagesAsync();
        content.Should().ContainSingle().Which.Should().Match<Index.MessageFrontend>(i => i.Text == message.Text);
    }

    [Fact]
    public async Task AddMultipleMessages_ShouldReturnMessagesInOrder()
    {
        var firstId = Guid.NewGuid();
        var secondId = Guid.NewGuid();

        await _client.PutAsJsonAsync($"api/messages/{firstId}", new Index.MessageFrontend(firstId, "First message", TimeProvider.System.GetUtcNow(), UpdatedAt: null));
        await _client.PutAsJsonAsync($"api/messages/{secondId}", new Index.MessageFrontend(secondId, "Second message", TimeProvider.System.GetUtcNow(), UpdatedAt: null));

        var content = await ListMessagesAsync();

        content.Should().SatisfyRespectively(
            firstMessage => firstMessage.Id.Should().Be(secondId),
            secondMessage => secondMessage.Id.Should().Be(firstId));
    }

    [Fact]
    public async Task UpdateMessage_ShouldReturnSingleUpdatedMessage()
    {
        var id = Guid.NewGuid();
        var firstMessage = new Index.MessageFrontend(id, "Hello, world!", TimeProvider.System.GetUtcNow(), UpdatedAt: null);
        var secondMessage = new Index.MessageFrontend(id, ":)", TimeProvider.System.GetUtcNow(), UpdatedAt: null);

        await _client.PutAsJsonAsync($"api/messages/{id}", firstMessage);
        await Task.Delay(2000);
        await _client.PutAsJsonAsync($"api/messages/{id}", secondMessage);

        var content = await ListMessagesAsync();

        content[0].Id.Should().Be(id);
        content[0].Text.Should().NotBe(firstMessage.Text);
        content[0].Text.Should().Be(secondMessage.Text);
        content[0].UpdatedAt.Should().NotBe(firstMessage.SentAt);
    }

    private async Task<IReadOnlyList<Index.MessageFrontend>> ListMessagesAsync()
    {
        var response = await _client.GetAsync("api/messages");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<IReadOnlyList<Index.MessageFrontend>>();
    }

    async Task IAsyncLifetime.InitializeAsync() => await fixture.ResetAsync();

    Task IAsyncLifetime.DisposeAsync() => Task.CompletedTask;
}
