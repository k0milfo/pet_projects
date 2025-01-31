namespace TelegramService.Service.Interfaces
{
	public interface ITelegramAlert
	{
		Task SendMessage(string message);
	}
}
