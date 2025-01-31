<<<<<<< HEAD
﻿using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Monitoring_Service.DAL.Interfaces;
using Monitoring_Service.DAL.Repositories;
using Monitoring_Service.Domain.Entity;
using Monitoring_Service.Service.Interfaces;
using System.Text.Json;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Monitoring_Service.Service.Implementations
{
	[ApiController]
	[Route("api/[controller]")]
	public class SensorDataChecker : ISensorDataChecker
	{
		private readonly ISensorDataRepository<SensorData> _sensorDataRepository;
		private readonly IRabbitMqService _rabbitMqService;
		//private readonly IKafkaProducer _kafkaProducer;
		public SensorDataChecker(ISensorDataRepository<SensorData> sensorDataRepository, IRabbitMqService rabbitMqService/*, IKafkaProducer kafkaProducer*/)
		{
			_sensorDataRepository = sensorDataRepository;
			_rabbitMqService = rabbitMqService;
			//_kafkaProducer = kafkaProducer;
		}

		public async Task Handle(string message)
		{
			//var sensorData = new SensorData(consumeResult.Message.Value);
			//if (IsCritical(sensorData))
			//{
			//	Console.WriteLine($"Данные получены в Handle и отправлены в БД и kafka от Device - {sensorData.DeviceId}");
			//	await _sensorDataRepository.Insert(sensorData);

			//	await _kafkaProducer.PostAsync(sensorData);
			//}
			try
			{

				var sensorData = JsonSerializer.Deserialize<SensorData>(message);

				if (sensorData == null)
				{
					Console.WriteLine("Сообщение не удалось преобразовать в SensorData");
					return;
				}

				if (!IsCritical(sensorData))
				{
					Console.WriteLine($"Критическое значение: {sensorData.Temperature}");
					await _sensorDataRepository.Insert(sensorData);
					Console.WriteLine("Данные сохранены в базу");
					await _rabbitMqService.SendMessage(message);
					Console.WriteLine("Данные отправлены в очередь alert-data");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Ошибка при обработке сообщения: {ex}");
			}
		}
		public bool IsCritical(SensorData data)
		{
			Console.WriteLine($"Выполняется проверка на критичность данных от Device - {data.DeviceId}");
			return data.Temperature < 50.0;

		}

		// метод для тестирования при отправке JSON
		//[HttpPost]
		//public async Task Post([FromBody] SensorData data)
		//{

		//		if (!IsCritical(data))
		//		{
		//		await _sensorDataRepository.Insert(data);
		//		//await _kafkaProducer.PostAsync(data);
		//		}

		//	}

		}
	}

=======
﻿using Confluent.Kafka;
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
>>>>>>> 7a9adf1f8c04b7d7e362b4edabbc562e1a0f96d9
