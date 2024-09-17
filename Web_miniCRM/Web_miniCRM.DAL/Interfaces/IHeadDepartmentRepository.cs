using Web_miniCRM.Domain.Entity;

namespace Web_miniCRM.DAL.Interfaces
{
    public interface IHeadDepartmentRepository : IBaseRepository<HeadDepartment>
    {
		Task<HeadDepartment> GetByDepartmentNumber(int id);
		Task<HeadDepartment> GetByEmail(string email);
		Task<HeadDepartment> GetNotFullByNumber(int id);
	}
}
