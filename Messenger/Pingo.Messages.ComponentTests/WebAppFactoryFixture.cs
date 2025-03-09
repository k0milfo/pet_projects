using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Pingo.Messages.Service;

namespace Pingo.Messages.ComponentTests;

public sealed class WebAppFactoryFixture
{
    public WebApplicationFactory<WebApi.Program> Factory;

    public WebAppFactoryFixture()
    {
        Factory = new WebApplicationFactory<WebApi.Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var ContextDbDescriptor = services.Single(d =>
                        d.ServiceType == typeof(IDbContextOptionsConfiguration<ApplicationDbContext>));
                    if (ContextDbDescriptor != null)
                    {
                        services.Remove(ContextDbDescriptor);
                    }

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDB");
                    });
                });
            });
    }

    public HttpClient CreateClient() => Factory.CreateClient();

    public async Task ResetAsync()
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

        if (dbContext != null)
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();
        }
    }
}
