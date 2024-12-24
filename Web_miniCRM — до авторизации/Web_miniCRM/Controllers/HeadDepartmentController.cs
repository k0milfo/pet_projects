using Microsoft.AspNetCore.Mvc;
using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Service.Implementations;
using Web_miniCRM.Service.Interfaces;

namespace Web_miniCRM.Controllers
{
	public class HeadDepartmentController : Controller
	{
		private readonly IHeadDepartmentService _headDepartmentService;
		private readonly IManagerService _managerService;
		public HeadDepartmentController(IHeadDepartmentService headDepartmentService, IManagerService managerService)
		{
			_headDepartmentService = headDepartmentService;
			_managerService = managerService;

		}

		[HttpGet]
		public async Task<IActionResult> CreateHeadDepartment(int? id)
		{
			var NewHeadDepartment = new HeadDepartment();

			return View(NewHeadDepartment);
		}

		[HttpPost]
		public async Task<IActionResult> UpsertHeadDepartment(HeadDepartment model)
		{
			if (ModelState.IsValid)
			{
				if (model.HeadDepartmentId == 0)
				{
					await _headDepartmentService.Insert(model);
				}
				else
				{
					await _headDepartmentService.Update(model.HeadDepartmentId, model);
				}
			}
			else
			{
				return RedirectToAction("Error");
			}
			return RedirectToAction("GetHeadDepartments");
		}

		[HttpGet]
		public async Task<IActionResult> GetHeadDepartments()
		{
			var headDepartments = await _headDepartmentService.GetHeadDepartments();
			if (headDepartments.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(headDepartments.Data);
			}
			return View("Error");
		}
		[HttpGet]
		public async Task<IActionResult> GetByDepartmentId(int id, string? startDateRange, string? endDateRange)
		{
			var responseDepartment = await _headDepartmentService.Get(id);

			DateTime? startDate = string.IsNullOrEmpty(startDateRange) ? null : DateTime.Parse(startDateRange);
			DateTime? endDate = string.IsNullOrEmpty(endDateRange) ? null : DateTime.Parse(endDateRange);

			List<Call> filteredCalls = responseDepartment.Data.Managers.SelectMany(i => i.Calls.Where(call
			=> (!startDate.HasValue || call.Date >= startDate)
			&& (!endDate.HasValue || call.Date <= endDate))).ToList();

			List<Meeting> filteredMeetings = responseDepartment.Data.Managers.SelectMany(i => i.Meetings.Where(meeting
			=> (!startDate.HasValue || meeting.Date >= startDate)
			&& (!endDate.HasValue || meeting.Date <= endDate))).ToList();

			List<Invoice> filteredInvoices = responseDepartment.Data.Managers.SelectMany(i => i.Invoices.Where(invoice
			=> (!startDate.HasValue || invoice.InvoiceDate >= startDate)
			&& (!endDate.HasValue || invoice.InvoiceDate <= endDate))).ToList();

			if (responseDepartment.StatusCode == Domain.Enum.StatusCode.OK)
			{
				var departmentViewModel = new DepartmentViewModelFiltering
				{
					HeadDepartment = responseDepartment.Data,
					FilteredCalls = filteredCalls,
					FilteredMeetings = filteredMeetings,
					FilteredInvoices = filteredInvoices
				};
				return View(departmentViewModel);
			}
			else
			{
				return View("Error");
			}
		}
		[HttpGet]
		public async Task<IActionResult> GetCompanyDepartment(int departmentNumber)
		{
			var responseDepartment = await _headDepartmentService.GetByDepartmentNumber(departmentNumber);
			var responseData = responseDepartment.Data;
			var companies = responseData.Managers.SelectMany(i => i.Companies).ToList();
			if (responseDepartment.StatusCode == Domain.Enum.StatusCode.OK)
			{
				var departmentViewModel = new DepartmentViewModelFiltering
				{
					HeadDepartment = responseDepartment.Data,
					FilteredCompanies = companies
				};
				return View(departmentViewModel);
			}
			else
			{
				return View("Error");
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetManagersDepartment(int departmentNumber)
		{
			var ManagersByDepartment = await _headDepartmentService.GetByDepartmentNumber(departmentNumber);
			if (ManagersByDepartment.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(ManagersByDepartment.Data);
			}
			else
			{
				return View("Error");
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetCallsDepartment(int departmentNumber, string? startDateRange, string? endDateRange)
		{
			var responseDepartment = await _headDepartmentService.GetByDepartmentNumber(departmentNumber);

			DateTime? startDate = string.IsNullOrEmpty(startDateRange) ? null : DateTime.Parse(startDateRange);
			DateTime? endDate = string.IsNullOrEmpty(endDateRange) ? null : DateTime.Parse(endDateRange);

			if (responseDepartment.StatusCode == Domain.Enum.StatusCode.OK)
			{
				List<Call> filteredCalls = responseDepartment.Data.Managers.SelectMany(i => i.Calls.Where(call
					=> (!startDate.HasValue || call.Date >= startDate)
					&& (!endDate.HasValue || call.Date <= endDate))).ToList();

				var departmentViewModel = new DepartmentViewModelFiltering
				{
					HeadDepartment = responseDepartment.Data,
					FilteredCalls = filteredCalls
				};
				return View(departmentViewModel);
			}
			else
			{
				//TempData["SuccessMessage"] = "Произошла ошибка загрузки данных, проверьте наличие зарегистрированных звонков";
				//return View("ErrorCalls", new { managerId = id });
				return View("Error");
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetMeetingsDepartment(int departmentNumber, string? startDateRange, string? endDateRange)
		{
			var responseDepartment = await _headDepartmentService.GetByDepartmentNumber(departmentNumber);

			DateTime? startDate = string.IsNullOrEmpty(startDateRange) ? null : DateTime.Parse(startDateRange);
			DateTime? endDate = string.IsNullOrEmpty(endDateRange) ? null : DateTime.Parse(endDateRange);

			if (responseDepartment.StatusCode == Domain.Enum.StatusCode.OK)
			{
				List<Meeting> filteredMeetings = responseDepartment.Data.Managers.SelectMany(i => i.Meetings.Where(meeting
					=> (!startDate.HasValue || meeting.Date >= startDate)
					&& (!endDate.HasValue || meeting.Date <= endDate))).ToList();

				var departmentViewModel = new DepartmentViewModelFiltering
				{
					HeadDepartment = responseDepartment.Data,
					FilteredMeetings = filteredMeetings
				};
				return View(departmentViewModel);
			}
			else
			{
				//TempData["SuccessMessage"] = "Произошла ошибка загрузки данных, проверьте наличие зарегистрированных звонков";
				//return View("ErrorCalls", new { managerId = id });
				return View("Error");
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetInvoicesDepartment(int departmentNumber, string? startDateRange, string? endDateRange)
		{
			var responseDepartment = await _headDepartmentService.GetByDepartmentNumber(departmentNumber);

			DateTime? startDate = string.IsNullOrEmpty(startDateRange) ? null : DateTime.Parse(startDateRange);
			DateTime? endDate = string.IsNullOrEmpty(endDateRange) ? null : DateTime.Parse(endDateRange);

			if (responseDepartment.StatusCode == Domain.Enum.StatusCode.OK)
			{
				List<Invoice> filteredInvoices = responseDepartment.Data.Managers.SelectMany(i => i.Invoices.Where(invoice
					=> (!startDate.HasValue || invoice.InvoiceDate >= startDate)
					&& (!endDate.HasValue || invoice.InvoiceDate <= endDate))).ToList();

				var departmentViewModel = new DepartmentViewModelFiltering
				{
					HeadDepartment = responseDepartment.Data,
					FilteredInvoices = filteredInvoices
				};
				return View(departmentViewModel);
			}
			else
			{
				//TempData["SuccessMessage"] = "Произошла ошибка загрузки данных, проверьте наличие зарегистрированных звонков";
				//return View("ErrorCalls", new { managerId = id });
				return View("Error");
			}
		}
	}
}
