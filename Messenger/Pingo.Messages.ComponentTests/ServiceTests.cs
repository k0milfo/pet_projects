using FluentAssertions;
using Pingo.Messages.WebApi.Entity;
using static FrontendMessage.Pages.Index;

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
        var message = new InputMessage("Hello, world!");

        await _client.PutAsJsonAsync($"api/messages/{id}", message);
        var content = await ListMessagesAsync();
        content.Should().ContainSingle().Which.Should().Match<MessageResponseWebApi>(i => i.Text == message.Text);
    }

    [Fact]
    public async Task AddMultipleMessages_ShouldReturnMessagesInOrder()
    {
        var firstId = Guid.NewGuid();
        var secondId = Guid.NewGuid();

        await _client.PutAsJsonAsync($"api/messages/{firstId}", new InputMessage("First message"));
        await Task.Delay(2000);
        await _client.PutAsJsonAsync($"api/messages/{secondId}", new InputMessage("Second message"));

        var content = await ListMessagesAsync();

        content.Should().SatisfyRespectively(
            firstMessage => firstMessage.Id.Should().Be(secondId),
            secondMessage => secondMessage.Id.Should().Be(firstId));
    }

    [Fact]
    public async Task UpdateMessage_ShouldReturnSingleUpdatedMessage()
    {
        var id = Guid.NewGuid();
        var firstMessage = new MessageWebApi("Hello, world!");
        var secondMessage = new MessageWebApi(":)");

        await _client.PutAsJsonAsync($"api/messages/{id}", firstMessage);
        var content = await ListMessagesAsync();
        var firstMessageTime = content[0].SentAt;
        await Task.Delay(2000);
        await _client.PutAsJsonAsync($"api/messages/{id}", secondMessage);

        content = await ListMessagesAsync();

        content[0].Id.Should().Be(id);
        content[0].Text.Should().NotBe(firstMessage.Text);
        content[0].Text.Should().Be(secondMessage.Text);
        content[0].UpdatedAt.Should().NotBe(firstMessageTime);
    }

    private async Task<IReadOnlyList<MessageResponseWebApi>> ListMessagesAsync()
    {
        var response = await _client.GetAsync("api/messages");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<IReadOnlyList<MessageResponseWebApi>>();
    }

    async Task IAsyncLifetime.InitializeAsync() => await fixture.ResetAsync();

    Task IAsyncLifetime.DisposeAsync() => Task.CompletedTask;
}
