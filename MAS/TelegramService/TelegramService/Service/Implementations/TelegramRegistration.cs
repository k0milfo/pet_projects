using Microsoft.AspNetCore.Mvc.ModelBinding;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramService.DAL.Interfaces;
using TelegramService.DAL.Repositories;
using TelegramService.Service.Interfaces;
using static Telegram.Bot.TelegramBotClient;

namespace TelegramService.Service.Implementations
{
	public class TelegramRegistration : ITelegramRegistration
	{
		private readonly ITelegramBotClient _botClient;
		private readonly IUserDataRepository _userDataRepository;
		private readonly IServiceScopeFactory _scopeFactory;
		private long? _currentChatId;
		private string _deviceId;
		private string? _deviceLocation;
		private static string _currentState;
		private readonly IServiceProvider _serviceProvider;

		public TelegramRegistration(string token, IServiceScopeFactory scopeFactory)
		{
			_botClient = new TelegramBotClient(token);
			_scopeFactory = scopeFactory;
		}
		//public TelegramRegistration(string token, IUserDataRepository userDataRepository)
		//{
		//	_botClient = new TelegramBotClient(token);
		//	_userDataRepository = userDataRepository;
		//}
		//public TelegramRegistration(IServiceProvider serviceProvider)
		//{
		//	_serviceProvider = serviceProvider;
		//}

		public void StartReceiving(CancellationToken cancellationToken)
		{
			_botClient.StartReceiving(UpdateHandler, ErrorHandler, cancellationToken: cancellationToken);
		}

		private async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
		{
			if (update.Type != UpdateType.Message || update.Message.Type != MessageType.Text) { return; }

			var chatId = update.Message.Chat.Id;
			var messageText = update.Message.Text;

			using (var scope = _scopeFactory.CreateScope())
			{
				var userDataRepository = scope.ServiceProvider.GetRequiredService<IUserDataRepository>();

				if (messageText == "/start")
				{
					await botClient.SendTextMessageAsync(chatId, "Добро пожаловать! Введите /registration для регистрации датчика.");
					return;
				}

				if (messageText == "/registration")
				{
					_currentChatId = chatId;
					_currentState = "awaiting_device_id";
					await botClient.SendTextMessageAsync(chatId, "Введите Device Id состоящий из 3-х цифр");
					return;
				}

				if (_currentChatId == chatId)
				{
					switch (_currentState)
					{
						case "awaiting_device_id":
							if (messageText.Length == 3 && !string.IsNullOrEmpty(messageText))
							{
								_deviceId = messageText;
								_currentState = "awaiting_device_location";
								await botClient.SendTextMessageAsync(chatId, "Напишите место расположения датчика");
							}
							else
							{
								await botClient.SendTextMessageAsync(chatId, "Некорректный Device Id. Введите 3 цифры.");
							}
							break;
						case "awaiting_device_location":
							if (!string.IsNullOrEmpty(messageText))
							{
								_deviceLocation = messageText;
								await userDataRepository.Insert(new Domain.Entity.User
								{
									ChatId = chatId,
									DeviceId = _deviceId,
									DeviceLocation = _deviceLocation
								});
								await botClient.SendTextMessageAsync(chatId, $"Регистрация завершена.");
								_currentState = null;
							}
							else
							{
								await botClient.SendTextMessageAsync(chatId, "Некорректное местоположение. Попробуйте еще раз.");
							}
							break;
					}
				}
				else
				{
					await botClient.SendTextMessageAsync(chatId, "Данные были введены некорректно");
					_currentChatId = null;
					messageText = "/start";
				}
			}
		}
		private Task ErrorHandler(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
		{
			Console.WriteLine($"Ошибка: {exception.Message}");
			return Task.CompletedTask;
		}
	}
}
