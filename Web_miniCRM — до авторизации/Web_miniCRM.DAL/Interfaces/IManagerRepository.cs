using Web_miniCRM.Domain.Entity;

namespace Web_miniCRM.DAL.Interfaces
{
	public interface IManagerRepository : IBaseRepository<Manager>
	{
		Task<List<Manager>> GetManagersByDepartmentId(int id);
	}
}
