using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Pingo.Messages.Entity;
using Pingo.Messages.Interfaces;

namespace Pingo.Messages.Repositories;

internal sealed class MessageDataRepository(ApplicationDbContext context, IMapper mapper, TimeProvider timeProvider) : IMessageDataRepository
{
    public async Task<Result<MessageService?>> GetAsync(Guid id)
    {
        var entity = await context.Messages.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        return mapper.Map<MessageService>(entity);
    }

    public async Task<Result<IReadOnlyList<MessageService>>> GetAllAsync()
    {
        var entity = await context.Messages.OrderByDescending(i => i.UpdatedAt ?? i.SentAt).ToListAsync();
        return Result.Success(mapper.Map<IReadOnlyList<MessageService>>(entity));
    }

    public async Task<Result<bool>> DeleteAsync(Guid id)
    {
        var entity = await context.Messages.FirstOrDefaultAsync(i => i.Id == id);
        if (entity == null)
        {
            return Result.Success(value: false);
        }

        context.Messages.Remove(entity);
        await context.SaveChangesAsync();
        return Result.Success(value: true);
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
            UpdatedAt = timeProvider.GetUtcNow(),
        };
        context.Update(message);
        await context.SaveChangesAsync();
    }
}
