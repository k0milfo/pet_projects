using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pingo.Identity.Service.Interface;

namespace Pingo.Identity.Service.Implementations;

internal sealed class HangfireJobRegistry(IServiceProvider serviceProvider) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();

        RecurringJob.AddOrUpdate(
            "cleanup-tokens",
            () => tokenService.ClearingInvalidTokensAsync(),
            Cron.Daily);

        return Task.CompletedTask;
    }
}
