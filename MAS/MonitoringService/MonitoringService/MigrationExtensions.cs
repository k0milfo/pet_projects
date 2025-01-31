using Microsoft.EntityFrameworkCore;
using Monitoring_Service.DAL;

namespace Monitoring_Service
{
	public static class MigrationExtensions
	{
		public static void ApplyMigrations(this IApplicationBuilder app)
		{
			using IServiceScope scope = app.ApplicationServices.CreateScope();
			using ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

			try
			{
				dbContext.Database.Migrate();
				Console.WriteLine("Migrations applied successfully.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error applying migrations: {ex.Message}");
				throw;
			}
		}

	}
}
