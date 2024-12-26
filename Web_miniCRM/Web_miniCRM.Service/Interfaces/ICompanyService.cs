using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Domain.Response;

namespace Web_miniCRM.Service.Interfaces
{
    public interface ICompanyService
	{
		Task<IBaseResponse<List<Company>>> GetCompanies();
		Task<IBaseResponse<Company>> GetCompanyId(int id);
		Task<IBaseResponse<Company>> GetCompanyByName(string name);
		Task<IBaseResponse<bool>> DeleteCompany(int id);
		Task<IBaseResponse<bool>> Insert(Company NewCompany);
		Task<IBaseResponse<Company>> Update(int id, Company NewCompany);
	}
}
