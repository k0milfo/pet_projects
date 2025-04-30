using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pingo.Messages.Entity;
using Pingo.Messages.Interfaces;
using Pingo.Messages.Repositories;

namespace Pingo.Messages.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMessages(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddScoped<IMessageDataRepository, MessageDataRepository>();
        services.AddScoped<IMessagesService, MessagesService>();
        services.AddAutoMapper(typeof(MappingProfileService));

        var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.Migrate();
    }
}
