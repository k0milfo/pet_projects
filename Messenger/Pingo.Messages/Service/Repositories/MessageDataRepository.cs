using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pingo.Messages.Entity;
using Pingo.Messages.Service.Interfaces;

namespace Pingo.Messages.Service.Repositories;

internal sealed class MessageDataRepository(ApplicationDbContext context, IMapper mapper) : IMessageDataRepository
{
    public async Task<MessageService?> GetAsync(Guid id)
    {
        var entity = await context.Messages.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        return mapper.Map<MessageService>(entity);
    }

    public async Task<IReadOnlyList<MessageService>> GetAllAsync()
    {
        var entity = await context.Messages.OrderByDescending(i => i.UpdatedAt ?? i.SentAt).ToListAsync();
        return mapper.Map<IReadOnlyList<MessageService>>(entity);
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

    public async Task InsertAsync(MessageService messagesEntity)
    {
        var message = mapper.Map<MessageRepository>(messagesEntity);
        await context.Messages.AddAsync(message);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MessageService messagesEntity)
    {
        var message = mapper.Map<MessageRepository>(messagesEntity);
        message = message with
        {
            UpdatedAt = TimeProvider.System.GetUtcNow(),
        };
        context.Update(message);
        await context.SaveChangesAsync();
    }
}
