using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pingo.Messages.Entity;
using Pingo.Messages.Service.Interfaces;
using Index = FrontendMessage.Pages.Index;

namespace Pingo.Messages.Service.Repositories;

internal sealed class MessageDataRepository(ApplicationDbContext context, IMapper mapper) : IMessageDataRepository<Index.MessageFrontend>
{
    public async Task<Index.MessageFrontend?> GetAsync(Guid id)
    {
        var entity = await context.Messages.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        return mapper.Map<Index.MessageFrontend>(entity);
    }

    public async Task<IReadOnlyList<Index.MessageFrontend>> GetAllAsync()
    {
        var entity = await context.Messages.OrderByDescending(i => i.UpdatedAt ?? i.SentAt).ToListAsync();
        return mapper.Map<IReadOnlyList<Index.MessageFrontend>>(entity);
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

    public async Task<bool> InsertAsync(Index.MessageFrontend entity)
    {
        var message = mapper.Map<Message>(entity);
        await context.Messages.AddAsync(message);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task UpdateAsync(Index.MessageFrontend entity)
    {
        var message = mapper.Map<Message>(entity);
        context.Update(message);
        await context.SaveChangesAsync();
    }
}
