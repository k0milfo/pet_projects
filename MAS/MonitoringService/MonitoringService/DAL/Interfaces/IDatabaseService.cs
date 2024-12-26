namespace Monitoring_Service.DAL.Interfaces
{
	public interface IDatabaseService<T>
	{
		Task<T> Get(int id);
		Task<List<T>> GetAll();
		Task<bool> Delete(int id);
		Task<bool> Insert(T entity);
	}
}
