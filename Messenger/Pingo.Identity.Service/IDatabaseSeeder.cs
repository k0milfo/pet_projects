namespace Pingo.Identity.Service;

public interface IDatabaseSeeder
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}
