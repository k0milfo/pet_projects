namespace Pingo.Messages.ComponentTests;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pingo.Messages.Entity;
using Pingo.Messages.Service;
using Pingo.Messages.Service.Interfaces;
using Pingo.Messages.Service.Repositories;

public sealed class WebAppFactoryFixture : IAsyncLifetime
{
    public WebAppFactoryFixture()
    {
        Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                    if (dbContextDescriptor != null)
                    {
                        services.Remove(dbContextDescriptor);
                    }

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDB");
                    });

                    var repositoryDescriptor = services.SingleOrDefault(d =>
                        d.ServiceType == typeof(IMessageDataRepository<Message>));
                    if (repositoryDescriptor != null)
                    {
                        services.Remove(repositoryDescriptor);
                    }

                    services.AddScoped<IMessageDataRepository<Message>, MessageDataRepository>();
                });
            });
    }

    public WebApplicationFactory<Program> Factory { get; private set; }

    public HttpClient CreateClient() => Factory.CreateClient();

    public async Task InitializeAsync()
    {
        using var scope = Factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
    }

    public Task DisposeAsync()
    {
        Factory.Dispose();
        return Task.CompletedTask;
    }
}
