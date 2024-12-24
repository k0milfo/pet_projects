using Web_miniCRM.Domain.Entity;

namespace Web_miniCRM.DAL.Interfaces
{
	public interface IMeetingRepository : IBaseRepository<Meeting>
	{
		Task<List<Meeting>> GetByManagerId(int id);
	}
}
