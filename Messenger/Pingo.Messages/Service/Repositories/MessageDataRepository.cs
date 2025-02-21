namespace Pingo.Messages.Service.Repositories;

using Microsoft.EntityFrameworkCore;

using Pingo.Messages.Entity;

using Pingo.Messages.Service.Interfaces;

public sealed class MessageDataRepository(ApplicationDbContext context) : IMessageDataRepository<Message>
{
    public async Task<Message?> GetAsync(Guid id)
    {
        return await context.Messages.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IList<Message>> GetAllAsync()
    {
        return await context.Messages.ToListAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await context.Messages.FirstOrDefaultAsync(i => i.Id == id);
        if (entity == null)
        {
            return false;
        }

        context.Messages.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> InsertAsync(Message entity)
    {
        await context.Messages.AddAsync(entity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(Message entity, Message updatedResponse)
    {
        context.Entry(entity).CurrentValues.SetValues(updatedResponse);
        await context.SaveChangesAsync();
        return true;
    }
}
