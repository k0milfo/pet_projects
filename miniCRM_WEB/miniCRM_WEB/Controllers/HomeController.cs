using Microsoft.AspNetCore.Mvc;
using miniCRM_WEB.Domain.Entity;
using System.Diagnostics;

namespace miniCRM_WEB.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			Company company = new Company()
			{
				CompanyName = "СтроительнаяКомпания 1"
			};
			return View(company);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		//[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		//public IActionResult Error()
		//{
		//	//return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		//}
	}
}
