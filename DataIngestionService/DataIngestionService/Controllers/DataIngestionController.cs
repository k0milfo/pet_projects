using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace DataIngestionService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DataIngestionController : ControllerBase
	{
		private readonly IProducer<string, string> _producer;
		private readonly string _topic;

		public DataIngestionController(IProducer<string, string> producer)
		{
			_producer = producer;
			_topic = "sensor-data";
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] SensorData data)
		{
			try
			{
				var message = new Message<string, string>
				{
					Key = data.DeviceId,
					Value = data.ToString()
				};

				var result = await _producer.ProduceAsync(_topic, message);
				return Ok(new { Message = "Data sent to Kafka", Result = result });
			}
			catch (ProduceException<string, string> e)
			{
				return StatusCode(500, new { Error = e.Error.Reason });
			}
		}
	}
	public class SensorData
	{
		public string DeviceId { get; set; }
		public double Temperature { get; set; }
		public double Humidity { get; set; }
		public string Timestamp { get; set; }

		public override string ToString()
		{
			return $"{{\"DeviceId\":\"{DeviceId}\", \"Temperature\":{Temperature}, \"Humidity\":{Humidity}, \"Timestamp\":\"{Timestamp}\"}}";
		}
	}
}
