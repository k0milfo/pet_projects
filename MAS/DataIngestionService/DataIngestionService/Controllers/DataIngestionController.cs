using Confluent.Kafka;
using DataIngestionService.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace DataIngestionService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DataIngestionController : ControllerBase
	{
		private readonly IRabbitMqService _mqService;

		public DataIngestionController(IRabbitMqService mqService)
		{
			_mqService = mqService;
		}

		[HttpPost]
		public IActionResult Post(string message)
		{
			_mqService.SendMessage(message);

			return Ok("Сообщение отправлено");
		}
	}
	public class SensorData
	{
		public int? id { get; set; }
		public string? DeviceId { get; set; }
		public double Temperature { get; set; }
		public double Humidity { get; set; }
		public string? DateTime { get; set; }

		public override string ToString()
		{
			return $"{{\"DeviceId\":\"{DeviceId}\", \"Temperature\":{Temperature}, \"Humidity\":{Humidity}, \"DateTime\":\"{DateTime}\"}}";
		}
	}
}
