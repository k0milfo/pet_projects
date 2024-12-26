using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Domain.Response;

namespace Web_miniCRM.Service.Interfaces
{
	public interface ICallService
	{
		Task<IBaseResponse<List<Call>>> GetCalls();
		Task<IBaseResponse<List<Call>>> GetByManagerId(int id);
		Task<IBaseResponse<Call>> GetCallById(int id);
		Task<IBaseResponse<Call>> Update(int id, Call call);
		Task<IBaseResponse<bool>> Delete(int id);
		Task<IBaseResponse<bool>> Insert(Call call);
	}
}
