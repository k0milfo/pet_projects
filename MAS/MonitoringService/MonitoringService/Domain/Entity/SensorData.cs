using Confluent.Kafka;
using System.Text.Json;

namespace Monitoring_Service.Domain.Entity
{
    public class SensorData
    {
        public SensorData(string json)
        {
            var data = JsonSerializer.Deserialize<SensorData>(json);
            if (data != null)
            {
                DeviceId = data.DeviceId;
                Temperature = data.Temperature;
                Humidity = data.Humidity;
				DateTime = data.DateTime;
            }
        }

        public int Id {  get; set; }
        public string DeviceId { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public string DateTime { get; set; }

		public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
