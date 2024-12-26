using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Monitoring_Service.Domain.Entity;
using Monitoring_Service.Service.Interfaces;

namespace Monitoring_Service.Service.Implementations
{
	public class KafkaProducer : IKafkaProducer
	{
		private readonly IProducer<string, string> _producer;
		private readonly string _topic;
		public KafkaProducer(IProducer<string, string> producer)
		{
			_producer = producer;
			_topic = "alert-data";
		}

		public async Task PostAsync(SensorData data)
		{
			try
			{
				var message = new Message<string, string>
				{
					Key = data.DeviceId,
					Value = data.ToString()
				};

				var result = await _producer.ProduceAsync(_topic, message);
			}
			catch (ProduceException<string, string> e)
			{
				Console.WriteLine($"Error producing message: {e.Error.Reason}");
				throw;
			}
		}
	}
}
