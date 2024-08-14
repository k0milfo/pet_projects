using Azure;
using Microsoft.AspNetCore.Mvc;
using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Service.Interfaces;

namespace Web_miniCRM.Controllers
{
	public class CompanyController : Controller
	{
		public readonly ICompanyService _companyServices;
		public readonly IManagerService _managerService;

		public CompanyController(ICompanyService companyServices, IManagerService managerService)
		{
			_companyServices = companyServices;
			_managerService = managerService;
		}

		[HttpGet]
		public async Task<IActionResult> GetCompanyInfo(int id)
		{
			var response = await _companyServices.GetCompanyId(id);

			if (response.StatusCode == Domain.Enum.StatusCode.OK && response.Data != null)
			{
				return View(response.Data);
			}
			else
			{
				return RedirectToAction("Error");
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetCompanies()
		{
			var responseCompanies = await _companyServices.GetCompanies();
			var responseManagers = await _managerService.GetManagers();

			if (responseCompanies.StatusCode == Domain.Enum.StatusCode.OK && responseManagers.StatusCode == Domain.Enum.StatusCode.OK)
			{
				//var viewModel = new CreateInvoiceViewModel
				//{
				//	_companies = responseCompanies.Data,
				//	_managers = responseManagers.Data
				//};
				var viewModel = new CreateInvoiceViewModel(responseCompanies.Data, null, null, responseManagers.Data);
				return View(viewModel);
			}
			return RedirectToAction("Error");
		}

		[HttpGet]
		public async Task<IActionResult> GetCompanyId(int id)
		{
			var response = await _companyServices.GetCompanyId(id);

			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			return RedirectToAction("Error");
		}

		[HttpGet]
		public async Task<IActionResult> GetCompanyByName(string name)
		{
			var response = await _companyServices.GetCompanyByName(name);

			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			return RedirectToAction("Error");
		}

		[HttpGet]
		public async Task<IActionResult> DeleteCompany(int id)
		{
			var response = await _companyServices.DeleteCompany(id);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return RedirectToAction("GetCompanies");
			}
			return RedirectToAction("Error");
		}

		[HttpPost]
		public async Task<IActionResult> UpsertCompany(Company model)
		{
			if (ModelState.IsValid)
			{
				if (model.CompanyId == 0)
				{
					await _companyServices.Insert(model);
				}
				else
				{
					await _companyServices.Update(model.CompanyId, model);
				}
			}
			else
			{
				var errors = ModelState.Values.SelectMany(v => v.Errors);
				foreach (var error in errors)
				{
					Console.WriteLine(error);
					// Обработка ошибок валидации
					// error.ErrorMessage содержит сообщение об ошибке
				}
			}
			return RedirectToAction("GetCompanies");
		}

		[HttpGet]
		public async Task<IActionResult> CreateCompany(int? id)
		{
			var NewCompany = new Company();
			NewCompany.Informations.Add(new Information { Details = "" });
			NewCompany.ManagerId = id.HasValue ? id : 4; // если id null - присваивается 4 (специальное значение для компаний без менеджера)

			return View(NewCompany);
		}
		[HttpGet]
		public async Task<IActionResult> ChangingDataCompany(int id)
		{
			var response = await _companyServices.GetCompanyId(id);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			return RedirectToAction("Error");
		}

		[HttpGet]
		public async Task<IActionResult> FindCompany(string item)
		{

			int id = 0;
			if (item.Length == 10 && item.All(char.IsDigit))
			{
				var repose = await _companyServices.GetCompanies();
				var company = repose.Data.FirstOrDefault(i => i.INN.Equals(item));
				if (company != null)
				{
					id = company.CompanyId;
				}
			}
			else
			{
				var repose = await _companyServices.GetCompanyByName(item);
				if (repose?.Data != null)
				{
					id = repose.Data.CompanyId;
				}
			}

			if (id != 0)
			{
				return RedirectToAction("GetCompanyInfo", "Company", new { id });
			}

			TempData["SuccessMessage"] = "Не удалось найти компанию";
			return RedirectToAction("GetCompanies", "Company");
		}
	}
}
