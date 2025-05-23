using Microsoft.EntityFrameworkCore;
using Pingo.Messages.Entity;

namespace Pingo.Messages;

internal sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<MessageEntity> Messages { get; set; }
}
