namespace Pingo.Messages.Service;

using Microsoft.EntityFrameworkCore;
using Pingo.Messages.Entity;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Message> Messages { get; set; }
}
