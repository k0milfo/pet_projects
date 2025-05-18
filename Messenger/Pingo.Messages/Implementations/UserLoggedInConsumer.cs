using MassTransit;
using Pingo.Identity.Events;
using Pingo.Messages.Interfaces;

namespace Pingo.Messages.Implementations;

internal sealed class UserLoggedInConsumer(IMessagesService messagesService) : IConsumer<UserLoggedIn>
{
    public async Task Consume(ConsumeContext<UserLoggedIn> context)
    {
        var messageId = context.MessageId ?? Guid.NewGuid();
        await messagesService.SendWelcomeMessageAsync(messageId, $"Привет, {context.Message.Email}!");
    }
}
