using Microsoft.AspNetCore.Mvc;
using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Service.Interfaces;
namespace Web_miniCRM.Controllers
{
	public class ManagerController : Controller
	{
		public readonly IManagerService _managerService;
		public readonly IInvoiceService _invoiceService;

		public ManagerController(IManagerService managerService, IInvoiceService invoiceService)
		{
			_managerService = managerService;
			_invoiceService = invoiceService;
		}
		[HttpGet]
		public async Task<IActionResult> GetManagers()
		{
			var response = await _managerService.GetManagers();
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			else
			{
				return View("Error");
			}
		}
		[HttpGet]
		public async Task<IActionResult> GetManagerInfoCompany(int id)
		{
			var response = await _managerService.Get(id);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			else
			{
				return View("Error");
			}
		}
		[HttpGet]
		public async Task<IActionResult> GetManagerInfoMeetings(int id)
		{
			var response = await _managerService.Get(id);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			else
			{
				return View("Error");
			}
		}

		//На всякий случай оставил метод, поиск звонков будет выполняться в контроллере CallController
		//[HttpGet]
		//public async Task<IActionResult> GetManagerInfoCalls(int id)
		//{
		//	var response = await _managerService.Get(id);
		//	if (response.StatusCode == Domain.Enum.StatusCode.OK)
		//	{
		//		return View(response.Data);
		//	}
		//	else
		//	{
		//		return View("Error");
		//	}
		//}

		[HttpGet]
		public async Task<IActionResult> GetManagerInfoInvoice(int id, string? startDateRange, string? endDateRange)
		{
			var responseManager = await _managerService.Get(id);

			DateTime? startDate = string.IsNullOrEmpty(startDateRange) ? null : DateTime.Parse(startDateRange);
			DateTime? endDate = string.IsNullOrEmpty(endDateRange) ? null : DateTime.Parse(endDateRange);
			List<Invoice> allInvoices = responseManager.Data.Companies.SelectMany(i => i.Invoices).ToList();

			if (responseManager.StatusCode == Domain.Enum.StatusCode.OK)
			{
				var filteredInvoice = (allInvoices.Where(invoice 
					=> (!startDate.HasValue || invoice.InvoiceDate >= startDate) 
					&& (!endDate.HasValue || invoice.InvoiceDate <= endDate)));

				var viewModel = new ManagerViewModel
				{
					Manager = responseManager.Data,
					FilteredInvoices = filteredInvoice,
				};
				return View(viewModel);
			}
			else
			{
				return View("Error");
			}
		}
	}
}
