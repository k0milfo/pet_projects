using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Domain.Response;

namespace Web_miniCRM.Service.Interfaces
{
	public interface IMeetingService
	{
		Task<IBaseResponse<List<Meeting>>> GetMeetings();
		Task<IBaseResponse<List<Meeting>>> GetByManagerId(int id);
		Task<IBaseResponse<Meeting>> GetMeetingId(int id);
		Task<IBaseResponse<Meeting>> Update(int id, Meeting meeting);
		Task<IBaseResponse<bool>> Delete(int id);
		Task<IBaseResponse<bool>> Insert(Meeting meeting);
	}
}
