using Microsoft.Extensions.DependencyInjection;
using Pingo.Identity.Service.Implementations;
using Pingo.Identity.Service.Interface;
using Pingo.Identity.Service.Repositories;

namespace Pingo.Identity.Service.Extensions;

public static class IdentityCollectionExtensions
{
    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddScoped<IIdentityRepository, IdentityRepository>();
        services.AddScoped<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
        services.AddHostedService<DatabaseMigrator>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ITokenService, TokenService>();
    }
}
