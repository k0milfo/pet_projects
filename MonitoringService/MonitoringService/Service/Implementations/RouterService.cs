using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Monitoring_Service.Service.Interfaces;

namespace Monitoring_Service.Service.Implementations
{
	[ApiController]
	[Route("api/[controller]")]
	public class RouterService : IRouterService
	{
		private readonly IConsumer<string, string> _consumer;
		private readonly ISensorDataChecker _checker;
		public RouterService(IConsumer<string, string> consumer, SensorDataChecker checker)
		{
			_consumer = consumer;
			_checker = checker;
		}

		public void RouteMessage(CancellationToken cancellationToken)
		{

			while (!cancellationToken.IsCancellationRequested)
			{
				var consumeResult = _consumer.Consume(cancellationToken);
				switch (consumeResult.Topic)
				{
					case "sensor-data":
						_checker.Handle(consumeResult);
						break;
				}
			}
		}
	}
}
