using Microsoft.AspNetCore.Mvc;
using Pingo.Messages.DataTransferObject;
using Pingo.Messages.Service.Interfaces;

namespace Pingo.Messages.WebApi.Controllers;

[ApiController]
[Route("api/messages")]
public sealed class MessagesController(IMessagesService service) : ControllerBase
{
    [HttpGet]
    public async Task<IReadOnlyList<MessagesEntityDto>> GetMessages()
    {
        return await service.GetMessagesAsync();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpsertMessage(Guid id, MessagesEntityDto messageApi)
    {
        await service.UpsertMessageAsync(id, messageApi);

        return NoContent();
    }
}
