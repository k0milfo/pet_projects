using Microsoft.EntityFrameworkCore;

namespace MessageService.Controllers
{
	public class MessageDbContext : DbContext
	{
		public DbSet<Message> Messages { get; set; }
		public MessageDbContext(DbContextOptions<MessageDbContext> options) : base(options)
		{
		}
	}
}
