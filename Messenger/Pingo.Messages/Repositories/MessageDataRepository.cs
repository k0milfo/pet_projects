using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pingo.Messages.Entity;
using Pingo.Messages.Interfaces;

namespace Pingo.Messages.Repositories;

internal sealed class MessageDataRepository(ApplicationDbContext context, IMapper mapper) : IMessageDataRepository
{
    public async Task<Message?> GetAsync(Guid id)
    {
        var entity = await context.Messages.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        return mapper.Map<Message>(entity);
    }

    public async Task<IReadOnlyList<Message>> GetAllAsync()
    {
        var entity = await context.Messages.OrderBy(i => i.UpdatedAt ?? i.SentAt).ToListAsync();
        return mapper.Map<IReadOnlyList<Message>>(entity);
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

    public async Task InsertAsync(Message messagesEntity)
    {
        var message = mapper.Map<MessageEntity>(messagesEntity);
        await context.Messages.AddAsync(message);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Message messagesEntity)
    {
        var message = mapper.Map<MessageEntity>(messagesEntity);
        context.Update(message);
        await context.SaveChangesAsync();
    }
}
