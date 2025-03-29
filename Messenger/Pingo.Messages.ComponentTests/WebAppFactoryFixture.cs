using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Pingo.Messages.ComponentTests;

public sealed class WebAppFactoryFixture
{
    private readonly WebApplicationFactory<WebApi.Program> _factory;

    public WebAppFactoryFixture()
    {
        _factory = new WebApplicationFactory<WebApi.Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var contextDbDescriptor = services.Single(d =>
                        d.ServiceType == typeof(IDbContextOptionsConfiguration<ApplicationDbContext>));
                    if (contextDbDescriptor != null)
                    {
                        services.Remove(contextDbDescriptor);
                    }

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDB");
                    });
                });
            });
    }

    public HttpClient CreateClient() => _factory.CreateClient();

    public async Task ResetAsync()
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

        if (dbContext != null)
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();
        }
    }
}
