using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pingo.Messages.DataTransferObject;
using Pingo.Messages.Entity;
using Pingo.Messages.Service.Interfaces;

namespace Pingo.Messages.Service.Repositories;

internal sealed class MessageDataRepository(ApplicationDbContext context, IMapper mapper) : IMessageDataRepository
{
    public async Task<MessagesEntityDto?> GetAsync(Guid id)
    {
        var entity = await context.Messages.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        return mapper.Map<MessagesEntityDto>(entity);
    }

    public async Task<IReadOnlyList<MessagesEntityDto>> GetAllAsync()
    {
        var entity = await context.Messages.OrderByDescending(i => i.UpdatedAt ?? i.SentAt).ToListAsync();
        return mapper.Map<IReadOnlyList<MessagesEntityDto>>(entity);
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

    public async Task InsertAsync(MessagesEntityDto messagesEntity)
    {
        var message = mapper.Map<Message>(messagesEntity);
        await context.Messages.AddAsync(message);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MessagesEntityDto messagesEntity)
    {
        var message = mapper.Map<Message>(messagesEntity);
        context.Update(message);
        await context.SaveChangesAsync();
    }
}
