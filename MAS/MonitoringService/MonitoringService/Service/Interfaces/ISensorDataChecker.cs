using Confluent.Kafka;
using Monitoring_Service.Domain.Entity;

namespace Monitoring_Service.Service.Interfaces
{
    public interface ISensorDataChecker
	{
		Task Handle(string message);
		bool IsCritical(SensorData data);
	}
}
