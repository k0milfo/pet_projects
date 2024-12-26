using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace MonitoringServiceDatabase.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class DataBaseController : ControllerBase
	{
		private readonly IConsumer<string, string> _consumer;

		public DataBaseController(IConsumer<string, string> consumer)
		{
			_consumer = consumer;
		}

		[HttpGet("consume")]
		public IActionResult ConsumeMessages(CancellationToken cancellationToken)
		{
			_consumer.Subscribe("sensor-data");

			try
			{
				while (true)
				{
					var consumeResult = _consumer.Consume(CancellationToken.None);
					Console.WriteLine($"Message received: {consumeResult.Message.Value}");
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
