using Microsoft.AspNetCore.Mvc;
using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Service.Implementations;
using Web_miniCRM.Service.Interfaces;

namespace Web_miniCRM.Controllers
{
	public class MeetingController : Controller
	{
		public readonly IMeetingService _meetingService;
		public readonly IManagerService _managerService;

		public MeetingController(IMeetingService meetingService, IManagerService managerService)
		{
			_meetingService = meetingService;
			_managerService = managerService;
		}

		[HttpGet]
		public async Task<IActionResult> GetMeetingsByManagerId(int id)
		{
			var response = await _meetingService.GetByManagerId(id);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			else
			{
				TempData["SuccessMessage"] = "Произошла ошибка загрузки данных, проверьте наличие зарегистрированных встреч";
				return View("ErrorMeetings", new { managerId = id });
				//return View("Error");
			}
		}

		//[HttpGet]
		//public async Task<IActionResult> GetMeetingsDataRange(int id, string? startDateRange, string? endDateRange)
		//{
		//	var responseMeetings = await _meetingService.GetByManagerId(id);

		//	DateTime? startDate = string.IsNullOrEmpty(startDateRange) ? null : DateTime.Parse(startDateRange);
		//	DateTime? endDate = string.IsNullOrEmpty(endDateRange) ? null : DateTime.Parse(endDateRange);

		//	if (responseMeetings.StatusCode == Domain.Enum.StatusCode.OK)
		//	{
		//		List<Meeting> filteredMeetings = (responseMeetings.Data.Where(meeting
		//			=> (!startDate.HasValue || meeting.Date >= startDate)
		//			&& (!endDate.HasValue || meeting.Date <= endDate))).ToList();

		//		return View("GetMeetingsByManagerId", filteredMeetings);
		//	}
		//	else
		//	{
		//		return View("Error");
		//	}
		//}
		[HttpGet]
		public async Task<IActionResult> CreateMeeting(int id)
		{
			var NewMeeting = new Meeting();
			var responseManager = await _managerService.Get(id);
			NewMeeting.Manager = responseManager.Data;
			NewMeeting.ManagerId = responseManager.Data.ManagerId;
			return View(NewMeeting);
		}
		[HttpPost]
		public async Task<IActionResult> UpsertMeeting(Meeting model)
		{
			var managerResponse = await _managerService.Get(model.ManagerId);
			model.Company = managerResponse.Data.Companies.FirstOrDefault(i => i.CompanyId == model.CompanyId);
			model.Manager = managerResponse.Data;

			if (ModelState.IsValid)
			{
				if (model.MeetingId == 0)
				{
					await _meetingService.Insert(model);
				}
				else
				{
					await _meetingService.Update(model.MeetingId, model);
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
			return RedirectToAction("GetMeetingsByManagerId", new { id = model.ManagerId });
		}
	}
}
