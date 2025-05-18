using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pingo.Messages.Entity;
using Pingo.Messages.Implementations;
using Pingo.Messages.Interfaces;
using Pingo.Messages.Repositories;
using IMessageDataRepository = Pingo.Messages.Interfaces.IMessageDataRepository;

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
        services.AddHostedService<MigrationHostedService>();
        services.AddScoped<UserLoggedInConsumer>();
        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserLoggedInConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                var settings = context.GetRequiredService<IOptions<RabbitMqSettings>>().Value;
                cfg.Host(settings.Host, h =>
                {
                    h.Username(settings.Login);
                    h.Password(settings.Password);
                });

                cfg.ReceiveEndpoint("user-logins-queue", ep =>
                {
                    ep.ConfigureConsumer<UserLoggedInConsumer>(context);
                });
            });
        });
    }
}
