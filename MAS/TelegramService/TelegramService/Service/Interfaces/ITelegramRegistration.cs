using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramService.Service.Interfaces
{
	public interface ITelegramRegistration
	{
		void StartReceiving(CancellationToken cancellationToken);
	}
}
