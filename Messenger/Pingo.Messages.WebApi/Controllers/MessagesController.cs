using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pingo.Messages.Interfaces;
using Pingo.Messages.WebApi.Entity;

namespace Pingo.Messages.WebApi.Controllers;

[ApiController]
[Route("api/messages")]
public sealed class MessagesController(IMessagesService service, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IReadOnlyList<MessageResponse>> GetMessages()
    {
        var messagesResponse = await service.GetMessagesAsync();
        return mapper.Map<IReadOnlyList<MessageResponse>>(messagesResponse.Value);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpsertMessage(Guid id, string message)
    {
        await service.UpsertMessageAsync(id, message);

        return NoContent();
    }
}
