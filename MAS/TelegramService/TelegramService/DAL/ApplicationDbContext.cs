using Microsoft.EntityFrameworkCore;
using TelegramService.Domain.Entity;

namespace TelegramService.DAL
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<User> UserData { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.Property(e => e.id)
				.ValueGeneratedOnAdd();
		}
	}
}
