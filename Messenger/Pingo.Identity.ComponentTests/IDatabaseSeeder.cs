namespace Pingo.Identity.ComponentTests;

public interface IDatabaseSeeder
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}
