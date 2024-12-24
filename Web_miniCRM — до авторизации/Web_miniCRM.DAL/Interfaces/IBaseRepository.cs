namespace Web_miniCRM.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> Get(int id);
        Task<List<T>> GetAll();
        Task<T> Update(T entity);
		Task<bool> Delete(T entity);
		Task<bool> Insert(T entity);
	}
}
