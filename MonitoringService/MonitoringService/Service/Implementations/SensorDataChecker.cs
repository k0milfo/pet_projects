using Confluent.Kafka;
using Monitoring_Service.Domain.Entity;
using Monitoring_Service.Service.Interfaces;

namespace Monitoring_Service.Service.Implementations
{
    public class SensorDataChecker : ISensorDataChecker
	{
		//private readonly IDatabaseService _databaseService;
		private readonly IKafkaProducer _kafkaProducer;
		//public SensorDataChecker(IDatabaseService databaseService, IKafkaProducer kafkaProducer)
		//{
		//	_databaseService = databaseService;
		//	_kafkaProducer = kafkaProducer;
		//}

		public void Handle(ConsumeResult<string, string> consumeResult)
		{
			var sensorData = new SensorData(consumeResult.Message.Value);
			if (IsCritical(sensorData))
			{
				//_databaseService.SaveCriticalData(sensorData);

				_kafkaProducer.PostAsync(sensorData);
			}
		}
		public bool IsCritical(SensorData data)
		{
			return data.Temperature > 50;

		}
	}
}
