using DataTransferObject;
using Microsoft.Extensions.DependencyInjection;
using Pingo.Identity.Service.Service.Implementations;
using Pingo.Identity.Service.Service.Interface;
using Pingo.Messages.WebApi.Entity;

namespace Pingo.Identity.Service.Service.Extensions;

public static class IdentityCollectionExtensions
{
    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddScoped<IIdentityRepository<EntityDto>, IdentityRepository>();
        services.AddScoped<IDatabaseConnection, DatabaseConnection>();
        services.AddScoped<DataBaseMigrator>();
        services.AddAutoMapper(typeof(MappingProfileIdentity));
        services.AddHostedService<DataBaseMigrator>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
    }
}
