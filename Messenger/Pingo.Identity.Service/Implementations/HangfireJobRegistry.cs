using Hangfire;
using Microsoft.Extensions.Hosting;
using Pingo.Identity.Service.Interface;

namespace Pingo.Identity.Service.Implementations;

internal sealed class HangfireJobRegistry(IRecurringJobManager jobManager) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        jobManager.AddOrUpdate<IIdentityService>(
            "cleanup-tokens",
            x => x.ClearingInvalidTokensAsync(),
            Cron.Daily);

        return Task.CompletedTask;
    }
}
