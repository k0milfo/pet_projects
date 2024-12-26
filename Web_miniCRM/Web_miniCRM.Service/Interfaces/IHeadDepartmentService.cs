using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Domain.Response;

namespace Web_miniCRM.Service.Interfaces
{
    public interface IHeadDepartmentService
    {
        Task<IBaseResponse<List<HeadDepartment>>> GetHeadDepartments();
        Task<IBaseResponse<HeadDepartment>> Get(int id);
		Task<IBaseResponse<HeadDepartment>> GetByDepartmentNumber(int id);
		Task<IBaseResponse<HeadDepartment>> Update(int id, HeadDepartment manager);
        Task<IBaseResponse<bool>> Insert(HeadDepartment NewManager);
        Task<IBaseResponse<bool>> DeleteById(int id);
		Task<IBaseResponse<bool>> DeleteByEmail(string Email);
		Task<IBaseResponse<HeadDepartment>> GetByEmail(string email);

	}
}
