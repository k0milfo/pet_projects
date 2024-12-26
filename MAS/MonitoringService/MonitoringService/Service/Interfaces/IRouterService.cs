using Microsoft.AspNetCore.Mvc;

namespace Monitoring_Service.Service.Interfaces
{
	public interface IRouterService
	{
		void RouteMessage(CancellationToken cancellationToken);
	}
}
