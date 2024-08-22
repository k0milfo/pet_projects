using Azure;
using Microsoft.AspNetCore.Mvc;
using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Service.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
			var companies = await _companyServices.GetCompanies();
			if(companies.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(companies.Data);
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

		[HttpGet]
		public async Task<IActionResult> GetCompanyInfoCalls(int id, string? startDateRange, string? endDateRange)
		{
			var company = await _companyServices.GetCompanyId(id);

			DateTime? startDate = string.IsNullOrEmpty(startDateRange) ? null : DateTime.Parse(startDateRange);
			DateTime? endDate = string.IsNullOrEmpty(endDateRange) ? null : DateTime.Parse(endDateRange);

			List<Call> filteredCalls = company.Data.Calls.Where(call
			=> (!startDate.HasValue || call.Date >= startDate)
			&& (!endDate.HasValue || call.Date <= endDate)).ToList();

			if (company.StatusCode == Domain.Enum.StatusCode.OK && company.Data != null)
			{
				var viewModel = new CompanyViewModelFiltering
				{
					Company = company.Data,
					FilteredCalls = filteredCalls
				};
				return View(viewModel);
			}
			else
			{
				return RedirectToAction("Error");
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetCompanyInfoMeetings(int id, string? startDateRange, string? endDateRange)
		{
			var company = await _companyServices.GetCompanyId(id);

			DateTime? startDate = string.IsNullOrEmpty(startDateRange) ? null : DateTime.Parse(startDateRange);
			DateTime? endDate = string.IsNullOrEmpty(endDateRange) ? null : DateTime.Parse(endDateRange);

			List<Meeting> filteredMeetings = company.Data.Meetings.Where(meeting
			=> (!startDate.HasValue || meeting.Date >= startDate)
			&& (!endDate.HasValue || meeting.Date <= endDate)).ToList();

			if (company.StatusCode == Domain.Enum.StatusCode.OK && company.Data != null)
			{
				var viewModel = new CompanyViewModelFiltering
				{
					Company = company.Data,
					FilteredMeetings = filteredMeetings
				};
				return View(viewModel);
			}
			else
			{
				return RedirectToAction("Error");
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetCompanyInfoInvoices(int id, string? startDateRange, string? endDateRange)
		{
			var company = await _companyServices.GetCompanyId(id);

			DateTime? startDate = string.IsNullOrEmpty(startDateRange) ? null : DateTime.Parse(startDateRange);
			DateTime? endDate = string.IsNullOrEmpty(endDateRange) ? null : DateTime.Parse(endDateRange);

			List<Invoice> filteredInvoices = company.Data.Invoices.Where(invoices
			=> (!startDate.HasValue || invoices.InvoiceDate >= startDate)
			&& (!endDate.HasValue || invoices.InvoiceDate <= endDate)).ToList();

			if (company.StatusCode == Domain.Enum.StatusCode.OK && company.Data != null)
			{
				var viewModel = new CompanyViewModelFiltering
				{
					Company = company.Data,
					FilteredInvoices = filteredInvoices
				};
				return View(viewModel);
			}
			else
			{
				return RedirectToAction("Error");
			}
		}
	}
}
