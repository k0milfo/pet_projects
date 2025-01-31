<<<<<<< HEAD
﻿using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Monitoring_Service.Service.Interfaces;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Monitoring_Service.Service.Implementations
{
	//[ApiController]
	//[Route("api/[controller]")]
	public class RouterService : BackgroundService/*, IRouterService*/
	{
		private readonly IServiceProvider _serviceProvider;
		//private IConsumer<string, string> _consumer;
		//private ISensorDataChecker _checker;
		//public RouterService(IServiceProvider serviceProvider)
		//{
		//	_serviceProvider = serviceProvider;
		//}

		//private readonly IModel _channel;
		private readonly IConnectionFactory _connectionFactory;
		//private readonly ISensorDataChecker _sensorDataChecker;
		private ISensorDataChecker _sensorDataChecker;
		public RouterService(IConnectionFactory connectionFactory, IServiceProvider serviceProvider/*ISensorDataChecker sensorDataChecker*/)
		{
			_connectionFactory = connectionFactory;
			_serviceProvider = serviceProvider;
			//_sensorDataChecker = sensorDataChecker;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			using (var scope = _serviceProvider.CreateScope())
			{
				//	_consumer = scope.ServiceProvider.GetRequiredService<IConsumer<string, string>>();
				_sensorDataChecker = scope.ServiceProvider.GetRequiredService<ISensorDataChecker>();

				//	_consumer.Subscribe("sensor-data");
				//	while (!stoppingToken.IsCancellationRequested)
				//	{
				//		var consumeResult = _consumer.Consume(stoppingToken);
				//		Console.WriteLine($"Получены данные в метод ExecuteAsync из топика - {consumeResult.Topic}");
				//		switch (consumeResult.Topic)
				//		{
				//			case "sensor-data":
				//				await _checker.Handle(consumeResult);
				//				break;
				//		}
				//	}
				//}

				try
				{
					using var connection = await _connectionFactory.CreateConnectionAsync();
					using var channel = await connection.CreateChannelAsync();
					await channel.QueueDeclareAsync(queue: "sensor-data", durable: false, exclusive: false, autoDelete: false,
						arguments: null);

					var consumer = new AsyncEventingBasicConsumer(channel);

					consumer.ReceivedAsync += async (sender, args) =>
					{
						var body = args.Body.ToArray();
						var message = Encoding.UTF8.GetString(body);

						Console.WriteLine($"Получено сообщение: {message}");

						await _sensorDataChecker.Handle(message);
						channel.BasicAckAsync(args.DeliveryTag, false);
					};
					channel.BasicConsumeAsync(queue: "sensor-data", autoAck: false, consumer: consumer);
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
=======
﻿using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Monitoring_Service.Service.Interfaces;

namespace Monitoring_Service.Service.Implementations
{
	[ApiController]
	[Route("api/[controller]")]
	public class RouterService : IRouterService
	{
		private readonly IConsumer<string, string> _consumer;
		private readonly ISensorDataChecker _checker;
		public RouterService(IConsumer<string, string> consumer, SensorDataChecker checker)
		{
			_consumer = consumer;
			_checker = checker;
		}

		public void RouteMessage(CancellationToken cancellationToken)
		{

			while (!cancellationToken.IsCancellationRequested)
			{
				var consumeResult = _consumer.Consume(cancellationToken);
				switch (consumeResult.Topic)
				{
					case "sensor-data":
						_checker.Handle(consumeResult);
						break;
				}
			}
		}
	}
}
>>>>>>> 7a9adf1f8c04b7d7e362b4edabbc562e1a0f96d9
