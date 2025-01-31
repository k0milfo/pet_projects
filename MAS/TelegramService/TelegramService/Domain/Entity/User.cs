namespace TelegramService.Domain.Entity
{
	public class User
	{
		//public User(long chatId, string? deviceId, string? deviceLocation)
		//{
		//	ChatId = chatId;
		//	DeviceId = deviceId;
		//	DeviceLocation = deviceLocation;
		//}

		public int? id { get; set; }
		public long? ChatId { get; set; }
		public string? DeviceId { get; set; }
		public string? DeviceLocation { get; set; }
	}
}
