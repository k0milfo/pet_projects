namespace Pingo.Messages.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Pingo.Messages.Entity;
using Pingo.Messages.Service.Interfaces;

[ApiController]
[Route("api/Messages")]
public sealed class MessagesController(IMessageDataRepository<Message> messageData, ILogger<MessagesController> logger) : ControllerBase
{
    public IMessageDataRepository<Message> MessageData { get; } = messageData;

    public ILogger<MessagesController> Logger { get; } = logger;

    [HttpGet]
    public async Task<IList<Message>> GetMessages()
    {
        var messages = await MessageData.GetAllAsync();
        return messages.OrderByDescending(i => i.UpdatedAt ?? i.SentAt).ToList();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Message>> UpsertMessage(Guid id, Message sentMessage)
    {
        var response = await MessageData.GetAsync(id);

        if (response == null)
        {
            await MessageData.InsertAsync(sentMessage);
        }
        else
        {
            var updatedResponse = sentMessage with
            {
                Id = response.Id,
                UpdatedAt = sentMessage.SentAt,
                Text = sentMessage.Text,
            };
            await MessageData.UpdateAsync(response, updatedResponse);
        }

        return this.Ok(sentMessage);
    }
}
