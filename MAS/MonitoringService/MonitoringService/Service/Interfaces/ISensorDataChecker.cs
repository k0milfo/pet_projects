<<<<<<< HEAD
﻿using Confluent.Kafka;
using Monitoring_Service.Domain.Entity;

namespace Monitoring_Service.Service.Interfaces
{
    public interface ISensorDataChecker
	{
		Task Handle(string message);
		bool IsCritical(SensorData data);
	}
}
=======
﻿using Confluent.Kafka;
using Monitoring_Service.Domain.Entity;

namespace Monitoring_Service.Service.Interfaces
{
    public interface ISensorDataChecker
	{
		void Handle(ConsumeResult<string, string> consumeResult);
		bool IsCritical(SensorData data);
	}
}
>>>>>>> 7a9adf1f8c04b7d7e362b4edabbc562e1a0f96d9
