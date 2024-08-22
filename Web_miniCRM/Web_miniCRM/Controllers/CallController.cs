using Microsoft.AspNetCore.Mvc;
using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Service.Interfaces;

namespace Web_miniCRM.Controllers
{
	public class CallController : Controller
	{
		public readonly ICallService _callService;
        public readonly IManagerService _managerService;

        public CallController(ICallService callService, IManagerService managerService)
		{
			_callService = callService;
            _managerService = managerService;
		}
		[HttpGet]
		public async Task<IActionResult> GetCallsByManagerId(int id)
		{
			var response = await _callService.GetByManagerId(id);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			else
			{
				TempData["SuccessMessage"] = "Произошла ошибка загрузки данных, проверьте наличие зарегистрированных звонков";
				return View("ErrorCalls", new { managerId = id });
				//return View("Error");
			}
		}

		//[HttpGet]
		//public async Task<IActionResult> GetCallsDataRange(int id, string? startDateRange, string? endDateRange)
		//{
		//	var responseCalls = await _callService.GetByManagerId(id);

		//	DateTime? startDate = string.IsNullOrEmpty(startDateRange) ? null : DateTime.Parse(startDateRange);
		//	DateTime? endDate = string.IsNullOrEmpty(endDateRange) ? null : DateTime.Parse(endDateRange);
			
		//	if (responseCalls.StatusCode == Domain.Enum.StatusCode.OK)
		//	{
		//		List<Call> filteredCalls = (responseCalls.Data.Where(call
		//			=> (!startDate.HasValue || call.Date >= startDate)
		//			&& (!endDate.HasValue || call.Date <= endDate))).ToList();

		//		//var viewModel = new ManagerViewModel
		//		//{
		//		//	Manager = responseManager.Data,
		//		//	FilteredInvoices = filteredInvoice,
		//		//};
		//		return View("GetCallsByManagerId", filteredCalls);
		//	}
		//	else
		//	{
		//		return View("Error");
		//	}
		//}

		[HttpGet]
		public async Task<IActionResult> CreateCall(int id)
		{
			var NewCall = new Call();
			var responseManager = await _managerService.Get(id);
            NewCall.Manager = responseManager.Data;
			NewCall.ManagerId = id;
			
			return View(NewCall);
		}

		[HttpPost]
		public async Task<IActionResult> UpsertCall(Call model)
		{
			var managerResponse = await _managerService.Get(model.ManagerId);
			model.Company = managerResponse.Data.Companies.FirstOrDefault(i => i.CompanyId == model.CompanyId);
			model.Manager = managerResponse.Data;

			if (ModelState.IsValid)
			{
				if (model.CallId == 0)
				{
					await _callService.Insert(model);
				}
				else
				{
					await _callService.Update(model.CallId, model);
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
			return RedirectToAction("GetCallsByManagerId", new {id = model.ManagerId});
		}
	}
}
