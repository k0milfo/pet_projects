<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;
using Monitoring_Service.Domain.Entity;

namespace Monitoring_Service.Service.Interfaces
{
	public interface IKafkaProducer
	{
		Task PostAsync(SensorData data);
	}
}
=======
﻿using Microsoft.AspNetCore.Mvc;
using Monitoring_Service.Domain.Entity;

namespace Monitoring_Service.Service.Interfaces
{
	public interface IKafkaProducer
	{
		Task PostAsync(SensorData data);
	}
}
>>>>>>> 7a9adf1f8c04b7d7e362b4edabbc562e1a0f96d9
