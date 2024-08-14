using Web_miniCRM.Domain.Entity;

namespace Web_miniCRM.DAL.Interfaces
{
	public interface ICallRepository : IBaseRepository<Call>
	{
		Task<List<Call>> GetByManagerId(int id);
	}
}
