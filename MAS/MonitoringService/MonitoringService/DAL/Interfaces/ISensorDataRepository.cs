using Monitoring_Service.Domain.Entity;

namespace Monitoring_Service.DAL.Interfaces
{
	public interface ISensorDataRepository<SensorData>
	{
		Task<SensorData> Get(int id);
		Task<List<SensorData>> GetAll();
		Task<bool> Delete(int id);
		Task<bool> Insert(SensorData entity);
	}
}
