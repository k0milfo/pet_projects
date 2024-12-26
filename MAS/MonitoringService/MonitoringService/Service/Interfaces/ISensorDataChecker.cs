using Confluent.Kafka;
using Monitoring_Service.Domain.Entity;

namespace Monitoring_Service.Service.Interfaces
{
    public interface ISensorDataChecker
	{
		void Handle(ConsumeResult<string, string> consumeResult);
		bool IsCritical(SensorData data);
	}
}
