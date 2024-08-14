using Web_miniCRM.Domain.Entity;

namespace Web_miniCRM.DAL.Interfaces
{
    public interface ICompanyRepository : IBaseRepository<Company>
	{
		Task<Company> GetByName(string name);

	}
}
