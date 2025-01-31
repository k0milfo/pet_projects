using System.Text.Json.Serialization;
using System.Text.Json;

namespace TelegramService.Domain.Entity
{
	public class AlertData
	{
		[JsonConstructor]
		public AlertData() { }
		public AlertData(string json)
		{
			var data = JsonSerializer.Deserialize<AlertData>(json);
			if (data != null)
			{
				DeviceId = data.DeviceId;
				Temperature = data.Temperature;
				Humidity = data.Humidity;
				DateTime = data.DateTime;
			}
		}
		public int? id { get; set; }
		//public int? chatId { get; set; }
		public string? DeviceId { get; set; }
		public double Temperature { get; set; }
		public double Humidity { get; set; }
		public string? DateTime { get; set; }

		public override string ToString()
		{
			return JsonSerializer.Serialize(this);
		}
	}
}
