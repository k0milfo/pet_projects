using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pingo.Messages.Entity;
using Pingo.Messages.Interfaces;
using Pingo.Messages.WebApi.Entity;

namespace Pingo.Messages.WebApi.Controllers;

[ApiController]
[Route("api/messages")]
public sealed class MessagesController(IMessagesService service, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IReadOnlyList<MessageResponseWebApi>> GetMessages()
    {
        var messagesResponse = await service.GetMessagesAsync();
        return mapper.Map<IReadOnlyList<MessageResponseWebApi>>(messagesResponse.Value);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpsertMessage(Guid id, MessageWebApi messageApi)
    {
        var messageService = mapper.Map<MessageService>(messageApi);
        await service.UpsertMessageAsync(id, messageService);

        return NoContent();
    }
}
