using Microsoft.EntityFrameworkCore;
using Pingo.Messages.Entity;

namespace Pingo.Messages.Service;

internal sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Message> Messages { get; set; }
}
