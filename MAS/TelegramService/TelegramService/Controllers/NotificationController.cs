using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Telegram.Bot;
using TelegramService.Service.Implementations;
using TelegramService.Service.Interfaces;

namespace TelegramService.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class NotificationController : BackgroundService
	{
		private ITelegramAlert _telegramService;
		private readonly IServiceProvider _serviceProvider;
		private readonly RabbitMQ.Client.IConnectionFactory _connectionFactory;
		private readonly ITelegramRegistration _telegramRegistration;
		public NotificationController(IServiceProvider serviceProvider, RabbitMQ.Client.IConnectionFactory connectionFactory, ITelegramRegistration telegramRegistration)
		{
			_serviceProvider = serviceProvider;
			_connectionFactory = connectionFactory;
			_telegramRegistration = telegramRegistration;
		}


		[HttpPost]
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_telegramRegistration.StartReceiving(stoppingToken);

			using (var scope = _serviceProvider.CreateScope())
			{
				_telegramService = scope.ServiceProvider.GetRequiredService<ITelegramAlert>();
				//_connectionFactory = scope.ServiceProvider.GetRequiredService<RabbitMQ.Client.IConnectionFactory>();
				try
				{
					using var connection = await _connectionFactory.CreateConnectionAsync();
					using var channel = await connection.CreateChannelAsync();
					await channel.QueueDeclareAsync(queue: "alert-data", durable: false, exclusive: false, autoDelete: false,
						arguments: null);
					var consumer = new AsyncEventingBasicConsumer(channel);
					consumer.ReceivedAsync += async (sender, args) =>
					{
						var body = args.Body.ToArray();
						var message = Encoding.UTF8.GetString(body);

						Console.WriteLine($"Получено сообщение: {message}");

						await _telegramService.SendMessage(message);
						channel.BasicAckAsync(args.DeliveryTag, false);
					};
					channel.BasicConsumeAsync(queue: "alert-data", autoAck: false, consumer: consumer);
					await Task.Delay(Timeout.Infinite, stoppingToken);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Ошибка в ExecuteAsync: {ex}");
				}
			}
		}
	}
}
