using Microsoft.AspNetCore.Mvc;
using Web_miniCRM.Service.Implementations;
using Web_miniCRM.Service.Interfaces;

namespace Web_miniCRM.Controllers
{
	public class MeetingController : Controller
	{
		public readonly IMeetingService _meetingService;

		public MeetingController(IMeetingService meetingService)
		{
			_meetingService = meetingService;
		}

		[HttpGet]
		public async Task<IActionResult> GetManagerInfoMeetings(int id)
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
	}
}
