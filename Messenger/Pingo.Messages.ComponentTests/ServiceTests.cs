using Microsoft.Extensions.DependencyInjection;
using Pingo.Messages.Entity;
using Pingo.Messages.Service;

namespace Pingo.Messages.ComponentTests;

[Collection("Database Collection")]
public sealed class ServiceTests : IClassFixture<WebAppFactoryFixture>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly ApplicationDbContext _dbContext;

    public ServiceTests(WebAppFactoryFixture fixture)
    {
        _client = fixture.CreateClient();
        var scope = fixture.Factory.Services.CreateScope();
        _dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    public async Task InitializeAsync()
    {
        await _dbContext.Database.EnsureDeletedAsync();
        await _dbContext.Database.EnsureCreatedAsync();
    }

    public Task DisposeAsync()
    {
        _dbContext.Dispose();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task GetAllMessages_ShouldReturnEmptyList()
    {
        var response = await _client.GetAsync("api/Messages");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsAsync<List<Message>>();
        Assert.Empty(content);
    }

    [Fact]
    public async Task AddOneMessage_ShouldReturnSingleMessage()
    {
        var id = Guid.NewGuid();
        var message = new Message(id, "Hello, world!", DateTime.UtcNow, UpdatedAt: null);

        await _client.PutAsJsonAsync($"api/Messages/{id}", message);
        var response = await _client.GetAsync("api/Messages");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsAsync<List<Message>>();
        Assert.Single(content);
    }

    [Fact]
    public async Task AddMultipleMessages_ShouldReturnMessagesInOrder()
    {
        var firstId = Guid.NewGuid();
        var secondId = Guid.NewGuid();

        await _client.PutAsJsonAsync($"api/Messages/{firstId}", new Message(firstId, "First message", DateTime.UtcNow, UpdatedAt: null));
        await _client.PutAsJsonAsync($"api/Messages/{secondId}", new Message(secondId, "Second message", DateTime.UtcNow, UpdatedAt: null));

        var response = await _client.GetAsync("api/Messages");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsAsync<List<Message>>();

        Assert.Collection(content, message => Assert.Equal(secondId, message.Id), message => Assert.Equal(firstId, message.Id));
    }

    [Fact]
    public async Task UpdateMessage_ShouldReturnSingleUpdatedMessage()
    {
        var id = Guid.NewGuid();
        var firstMessage = new Message(id, "Hello, world!", DateTime.UtcNow, UpdatedAt: null);
        var secondMessage = new Message(id, ":)", DateTime.UtcNow, UpdatedAt: null);

        await _client.PutAsJsonAsync($"api/Messages/{id}", firstMessage);
        await _client.PutAsJsonAsync($"api/Messages/{id}", secondMessage);

        var response = await _client.GetAsync("api/Messages");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsAsync<List<Message>>();

        Assert.Equal(id, content[0].Id);
        Assert.NotEqual(firstMessage.Text, content[0].Text);
        Assert.NotEqual(firstMessage.UpdatedAt, content[0].UpdatedAt);
    }
}
