namespace DataIngestionService.RabbitMQ
{
	public interface IRabbitMqService
	{
		void SendMessage(object obj);
		Task SendMessage(string message);
	}
}
