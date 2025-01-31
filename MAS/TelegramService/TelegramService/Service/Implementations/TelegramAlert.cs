using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Telegram.Bot;
using TelegramService.DAL.Interfaces;
using TelegramService.Domain.Entity;
using TelegramService.Service.Interfaces;


namespace TelegramService.Service.Implementations
{
	public class TelegramAlert : ITelegramAlert/*, IHostedService*/
	{
		private readonly TelegramBotClient _botClient;
		private readonly IServiceScopeFactory _scopeFactory;
		public TelegramAlert(IServiceScopeFactory scopeFactory)
		{
			_botClient = new TelegramBotClient("7729053840:AAEy_AM_QnowZWiwCVJ-bXBgMJqmKxIeFVw");
			_scopeFactory = scopeFactory;
		}

		//[HttpPost]
		public async Task SendMessage(string message)
		{
			using (var scope = _scopeFactory.CreateScope())
			{
				var userDataRepository = scope.ServiceProvider.GetRequiredService<IUserDataRepository>();
				Console.WriteLine("Получены данные в SendMessage класса TelegramAlert");
				var alterData = JsonSerializer.Deserialize<AlertData>(message);

				if (string.IsNullOrEmpty(message))
				{
					Console.WriteLine("Некорректные данные.");
				}
				var user = userDataRepository.GetDeviceId(alterData.DeviceId);
				Console.WriteLine($"Получен из БД chatid {user.Result.ChatId} и devicelocation {user.Result.DeviceLocation}");
				await _botClient.SendTextMessageAsync(user.Result.ChatId, $"Уведомление о критических данных в локации {user.Result.DeviceLocation} Temperature: {alterData.Temperature.ToString()} {alterData.DateTime}");
				Console.WriteLine("Уведомление отправлено.");
			}
		}
	}
}
