using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace DataIngestionService.RabbitMQ
{
	public class RabbitMqService : IRabbitMqService
	{
		public void SendMessage(object obj)
		{
			var message = JsonSerializer.Serialize(obj);
			SendMessage(message);
		}

		public async Task SendMessage(string message)
		{
			var factory = new ConnectionFactory { HostName = "rabbitmq" };
			using var connection = await factory.CreateConnectionAsync();
			using var channel = await connection.CreateChannelAsync();
			{
				channel.QueueDeclareAsync(queue: "sensor-data",
							   durable: false,
							   exclusive: false,
							   autoDelete: false,
							   arguments: null);

				var body = Encoding.UTF8.GetBytes(message);

				await channel.BasicPublishAsync(exchange: string.Empty,
							   routingKey: "sensor-data",
							   body: body);
			}
		}
	}
}
