using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Domain.Response;

namespace Web_miniCRM.Service.Interfaces
{
	public interface IManagerService
	{
		Task<IBaseResponse<List<Manager>>> GetManagers();
		Task<IBaseResponse<Manager>> Get(int id);
		Task<IBaseResponse<Manager>> Update(int id, Manager manager);
		Task<IBaseResponse<bool>> Insert(Manager NewManager);
		Task<IBaseResponse<bool>> Delete(int id);
		Task<IBaseResponse<List<Manager>>> GetManagersByDepartmentId(int id);
	}
}
