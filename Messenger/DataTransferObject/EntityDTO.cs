namespace DataTransferObject;

public sealed record EntityDto(Guid? Id, DateTimeOffset SentAt, string Email, string HashPassword, string Salt);
