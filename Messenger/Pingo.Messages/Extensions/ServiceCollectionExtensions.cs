using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pingo.Messages.Entity;
using Pingo.Messages.Service;
using Pingo.Messages.Service.Interfaces;
using Pingo.Messages.Service.Repositories;

namespace Pingo.Messages.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMessages(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddScoped<IMessageDataRepository, MessageDataRepository>();
        services.AddScoped<IMessagesService, MessagesService>();
        services.AddAutoMapper(typeof(MappingProfile));
    }
}
