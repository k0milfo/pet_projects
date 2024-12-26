using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace MonitoringServiceProducer.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class MonitoringController : ControllerBase
	{
		private readonly IConsumer<string, string> _consumer;

		public MonitoringController(IConsumer<string, string> consumer)
		{
			_consumer = consumer;
		}
		public IActionResult ConsumeMessages(CancellationToken cancellationToken)
		{
			_consumer.Subscribe("sensor-data");

			try
			{
				while (!cancellationToken.IsCancellationRequested)
				{
					var consumeResult = _consumer.Consume(cancellationToken);
					var messageResult = consumeResult.Message.Value;
				}
			}
			catch (ConsumeException ex)
			{
				Console.WriteLine($"Error: {ex.Error.Reason}");
			}

			return Ok("Started consuming messages");
		}
	}
}
