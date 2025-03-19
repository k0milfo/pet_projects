using AutoMapper;
using Pingo.Messages.DataTransferObject;
using Pingo.Messages.Service.Interfaces;

namespace Pingo.Messages.Service.Repositories;

internal sealed class MessagesService(IMessageDataRepository repository, IMapper mapper) : IMessagesService
{
    public async Task<IReadOnlyList<MessagesEntityDto>> GetMessagesAsync()
    {
        var messages = await repository.GetAllAsync();

        return mapper.Map<IReadOnlyList<MessagesEntityDto>>(messages);
    }

    public async Task UpsertMessageAsync(Guid id, MessagesEntityDto entity)
    {
        var oldMessage = await repository.GetAsync(id);

        if (oldMessage == null)
        {
            entity = entity with
            {
                Id = id,
            };
            await repository.InsertAsync(entity);
        }
        else
        {
            oldMessage = mapper.Map(entity, oldMessage);
            await repository.UpdateAsync(oldMessage);
        }
    }
}
