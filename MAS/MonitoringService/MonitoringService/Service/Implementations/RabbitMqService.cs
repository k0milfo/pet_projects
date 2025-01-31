using Monitoring_Service.Service.Interfaces;
using RabbitMQ.Client;
using System.Text;

namespace Monitoring_Service.Service.Implementations
{
	public class RabbitMqService : IRabbitMqService
	{
		public async Task SendMessage(string message)
		{
			var factory = new ConnectionFactory { HostName = "rabbitmq" };
			using var connection = await factory.CreateConnectionAsync();
			using var channel = await connection.CreateChannelAsync();
			{
				channel.QueueDeclareAsync(queue: "alert-data",
							   durable: false,
							   exclusive: false,
							   autoDelete: false,
							   arguments: null);

				var body = Encoding.UTF8.GetBytes(message);

				await channel.BasicPublishAsync(exchange: string.Empty,
							   routingKey: "alert-data",
							   body: body);
			}
		}
	}
}
