namespace Pingo.Messages.WebApi.Entity;

public record MessageResponseWebApi(Guid Id, string Text, DateTimeOffset SentAt, DateTimeOffset? UpdatedAt);
