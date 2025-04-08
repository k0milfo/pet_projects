using FluentAssertions;
using Pingo.Messages.WebApi.Entity;

namespace Pingo.Messages.ComponentTests;

[Collection(DatabaseCollection.NonParallelTests)]
public sealed class ServiceTests(WebAppFactoryFixture fixture) : IClassFixture<WebAppFactoryFixture>, IAsyncLifetime
{
    private readonly HttpClient _client = fixture.CreateClient();

    [Fact]
    public async Task GetAllMessages_ShouldReturnEmptyList()
    {
        // Arrange
        var content = await ListMessagesAsync();

        // Assert
        content.Should().BeEmpty();
    }

    [Fact]
    public async Task AddOneMessage_ShouldReturnSingleMessage()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        await _client.PutAsJsonAsync($"api/messages/{id}", "Hello, world!");

        // Arrange
        var content = await ListMessagesAsync();

        // Assert
        content.Should().ContainSingle().Which.Should().Match<MessageResponse>(i => i.Text == "Hello, world!");
    }

    [Fact]
    public async Task AddMultipleMessages_ShouldReturnMessagesInOrder()
    {
        // Arrange
        var firstId = Guid.NewGuid();
        var secondId = Guid.NewGuid();

        // Act
        await _client.PutAsJsonAsync($"api/messages/{firstId}", "First message");
        await _client.PutAsJsonAsync($"api/messages/{secondId}", "Second message");

        // Arrange
        var content = await ListMessagesAsync();

        // Assert
        content.Should().SatisfyRespectively(
            firstMessage => firstMessage.Id.Should().Be(secondId),
            secondMessage => secondMessage.Id.Should().Be(firstId));
    }

    [Fact]
    public async Task UpdateMessage_ShouldReturnSingleUpdatedMessage()
    {
        // Arrange
        var id = Guid.NewGuid();
        var firstMessage = new MessageDto("Hello, world!");
        var secondMessage = new MessageDto(":)");

        // Act
        await _client.PutAsJsonAsync($"api/messages/{id}", firstMessage);

        // Arrange
        var content = await ListMessagesAsync();
        var firstMessageTime = content[0].SentAt;

        // Act
        await _client.PutAsJsonAsync($"api/messages/{id}", secondMessage);

        // Arrange
        content = await ListMessagesAsync();

        // Assert
        content[0].Id.Should().Be(id);
        content[0].Text.Should().NotBe(firstMessage.Text);
        content[0].Text.Should().Be(secondMessage.Text);
        content[0].UpdatedAt.Should().NotBe(firstMessageTime);
    }

    private async Task<IReadOnlyList<MessageResponse>> ListMessagesAsync()
    {
        var response = await _client.GetAsync("api/messages");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<IReadOnlyList<MessageResponse>>();
    }

    async Task IAsyncLifetime.InitializeAsync() => await fixture.ResetAsync();

    Task IAsyncLifetime.DisposeAsync() => Task.CompletedTask;
}
