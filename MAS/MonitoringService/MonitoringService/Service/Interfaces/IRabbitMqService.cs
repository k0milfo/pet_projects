namespace Monitoring_Service.Service.Interfaces
{
	public interface IRabbitMqService
	{
		Task SendMessage(string message);
	}
}
