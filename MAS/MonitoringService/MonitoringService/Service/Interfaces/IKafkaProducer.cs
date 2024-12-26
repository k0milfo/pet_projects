using Microsoft.AspNetCore.Mvc;
using Monitoring_Service.Domain.Entity;

namespace Monitoring_Service.Service.Interfaces
{
	public interface IKafkaProducer
	{
		Task PostAsync(SensorData data);
	}
}
