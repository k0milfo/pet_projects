using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Web_miniCRM.DAL
{
	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
	{
		public ApplicationDbContext CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Web_miniCRM")) // Путь к корневой папке главного проекта
				.AddJsonFile("appsettings.json")
				.Build();

			var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
			var connectionString = configuration.GetConnectionString("DefaultConnection");

			builder.UseSqlServer("Server=172.19.0.2,1433;Database=miniCRM;User Id=sa;Password=YourStrong!Password;Encrypt=False;");

			return new ApplicationDbContext(builder.Options);
		}
	}
}
