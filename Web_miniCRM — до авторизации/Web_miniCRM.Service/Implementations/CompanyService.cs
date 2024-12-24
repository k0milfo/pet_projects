using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Domain.Response;
using Web_miniCRM.Service.Interfaces;
using Web_miniCRM.DAL.Interfaces;
using Web_miniCRM.Domain.Enum;

namespace Web_miniCRM.Service.Implementations
{
	public class CompanyService : ICompanyService
	{
		private readonly ICompanyRepository _companyRepository;
		public CompanyService(ICompanyRepository companyRepository)
		{
			_companyRepository = companyRepository;
		}

		public async Task<IBaseResponse<Company>> GetCompanyId(int id)
		{
			var baseResponse = new BaseResponse<Company>();

			try
			{
				var company = await _companyRepository.Get(id);
				if (company == null)
				{
					baseResponse.Description = "Элемент не найден";
					baseResponse.StatusCode = StatusCode.OK;

					return baseResponse;
				}
				baseResponse.Data = company;
				baseResponse.StatusCode = StatusCode.OK;

				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<Company>()
				{
					Description = $"[GetCompanyId] : {ex.Message}"
				};
			}
		}

		public async Task<IBaseResponse<Company>> GetCompanyByName(string name)
		{
			var baseResponse = new BaseResponse<Company>();

			try
			{
				var company = await _companyRepository.GetByName(name);
				if (company == null)
				{
					baseResponse.Description = "Элемент не найден";
					baseResponse.StatusCode = StatusCode.OK;

					return baseResponse;
				}
				baseResponse.Data = company;
				baseResponse.StatusCode = StatusCode.OK;

				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<Company>()
				{
					Description = $"[GetCompanyByName] : {ex.Message}"
				};
			}
		}

		public async Task<IBaseResponse<List<Company>>> GetCompanies()
		{
			var baseResponse = new BaseResponse<List<Company>>();
			try
			{
				var companies = await _companyRepository.GetAll();
				if (companies.Count == 0)
				{
					baseResponse.Description = "Найдено 0 элементов";
					baseResponse.StatusCode = StatusCode.OK;
					return baseResponse;
				}
				baseResponse.Data = companies;
				baseResponse.StatusCode = StatusCode.OK;

				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<Company>>()
				{
					Description = $"[GetCompanies] : {ex.Message}"
				};
			}
		}
		public async Task<IBaseResponse<Company>> Update(int id, Company NewCompany)
		{
			var baseResponse = new BaseResponse<Company>();
			try
			{
				var company = await _companyRepository.Get(id);
				if(company == null)
				{
					baseResponse.StatusCode = StatusCode.InternalServerError;
					baseResponse.Description = "Company not found";
					return baseResponse;
				}
				company.NameContactPerson = NewCompany.NameContactPerson;
				company.PhoneNumber = NewCompany.PhoneNumber;
				company.Email = NewCompany.Email;
				company.Informations = new List<Information>(NewCompany.Informations);

				await _companyRepository.Update(company);

				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<Company>()
				{
					Description = $"[GetCompanies] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}

		}

		public async Task<IBaseResponse<bool>> DeleteCompany(int id)
		{
			var baseResponse = new BaseResponse<bool>();

			try
			{
				var company = await _companyRepository.Get(id);
				if (company == null)
				{
					baseResponse.Description = "Элемент не найден";
					baseResponse.StatusCode = StatusCode.OK;
					baseResponse.Data = false;
				}
				else
				{
					await _companyRepository.Delete(company);
					baseResponse.Data = true;
					baseResponse.StatusCode = StatusCode.OK;
					baseResponse.Description = "Элемент удален";
				}

				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Description = $"[DeleteCompany] : {ex.Message}",
					StatusCode = StatusCode.OK
				};
			}
		}

		public async Task<IBaseResponse<bool>> Insert(Company NewCompany)
		{
			var baseResponse = new BaseResponse<bool>()
			{
				Data = true
			};

			try
			{
				await _companyRepository.Insert(NewCompany);
				//var company = await _companyRepository.Create(NewCompany);
				//if (company == null)
				//{
				//	baseResponse.Description = "Элемент не найден";
				//	baseResponse.StatusCode = StatusCode.OK;
				//	baseResponse.Data = false;

				//	return baseResponse;
				//}

				//await _companyRepository.Delete(company);

				return baseResponse;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in InsertCompany: {ex.Message}");

				return new BaseResponse<bool>()
				{
					Description = $"[InsertCompany] : {ex.Message}",
					StatusCode = StatusCode.OK,
					Data = false
				};
			}
		}
	}
}
