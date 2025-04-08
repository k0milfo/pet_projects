namespace Pingo.Messages.WebApi.Entity;

public record MessageResponse(Guid Id, string Text, DateTimeOffset SentAt, DateTimeOffset? UpdatedAt);
