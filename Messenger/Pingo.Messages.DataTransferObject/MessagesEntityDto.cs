namespace Pingo.Messages.DataTransferObject;

public sealed record MessagesEntityDto(Guid? Id, string? Text, DateTimeOffset? SentAt, DateTimeOffset? UpdatedAt);
