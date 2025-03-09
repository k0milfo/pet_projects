using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pingo.Messages.Service;
using Pingo.Messages.Service.Interfaces;
using Pingo.Messages.Service.Repositories;
using Index = FrontendMessage.Pages.Index;

namespace Pingo.Messages.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMessages(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IMessageDataRepository<Index.MessageFrontend>, MessageDataRepository>();
    }
}
