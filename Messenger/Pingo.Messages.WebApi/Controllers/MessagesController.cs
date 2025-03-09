using Microsoft.AspNetCore.Mvc;
using Pingo.Messages.WebApi.Service.Interface;
using Index = FrontendMessage.Pages.Index;

namespace Pingo.Messages.WebApi.Controllers;

[ApiController]
[Route("api/messages")]
public sealed class MessagesController(IMessagesService<Index.MessageFrontend> service) : ControllerBase
{
    [HttpGet]
    public async Task<IReadOnlyList<Index.MessageFrontend>> GetMessages()
    {
        return await service.GetMessagesAsync();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpsertMessage(Guid id, Index.MessageFrontend messageApi)
    {
        await service.UpsertMessageAsync(id, messageApi);

        return this.NoContent();
    }
}
