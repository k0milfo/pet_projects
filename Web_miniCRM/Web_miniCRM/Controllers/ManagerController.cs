using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Service.Interfaces;
namespace Web_miniCRM.Controllers
{
	public class ManagerController : Controller
	{
		public readonly IManagerService _managerService;
		public readonly IInvoiceService _invoiceService;
		public readonly IHeadDepartmentService _headDepartmentService;
        private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		public ManagerController(IManagerService managerService, IInvoiceService invoiceService, UserManager<ApplicationUser> userManager, IHeadDepartmentService headDepartmentService, SignInManager<ApplicationUser> signInManager)
		{
			_managerService = managerService;
			_invoiceService = invoiceService;
            _userManager = userManager;
			_headDepartmentService = headDepartmentService;
			_signInManager = signInManager;

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
		public async Task<IActionResult> GetManagersByDepartmentId(int id)
		{
			var response = await _managerService.GetManagersByDepartmentId(id);
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
		public async Task<IActionResult> GetManagerInfoInvoice(int id, string? startDateRange, string? endDateRange)
		{
			var manager = await _managerService.Get(id);

			DateTime? startDate = string.IsNullOrEmpty(startDateRange) ? null : DateTime.Parse(startDateRange);
			DateTime? endDate = string.IsNullOrEmpty(endDateRange) ? null : DateTime.Parse(endDateRange);
			List<Invoice> allInvoices = manager.Data.Companies.SelectMany(i => i.Invoices).ToList();

			if (manager.StatusCode == Domain.Enum.StatusCode.OK)
			{
				var filteredInvoice = (allInvoices.Where(invoice
					=> (!startDate.HasValue || invoice.InvoiceDate >= startDate)
					&& (!endDate.HasValue || invoice.InvoiceDate <= endDate))).ToList();

				var managerViewModel = new ManagerViewModelFiltering
				{
					Manager = manager.Data,
					FilteredInvoices = filteredInvoice,
				};
				return View(managerViewModel);
			}
			else
			{
				return View("Error");
			}
		}
		[HttpGet]
		public async Task<IActionResult> GetManagerInfoCalls(int id, string? startDateRange, string? endDateRange)
		{
			var manager = await _managerService.Get(id);

			DateTime? startDate = string.IsNullOrEmpty(startDateRange) ? null : DateTime.Parse(startDateRange);
			DateTime? endDate = string.IsNullOrEmpty(endDateRange) ? null : DateTime.Parse(endDateRange);

			if (manager.StatusCode == Domain.Enum.StatusCode.OK)
			{
				List<Call> filteredCalls = (manager.Data.Calls.Where(call
					=> (!startDate.HasValue || call.Date >= startDate)
					&& (!endDate.HasValue || call.Date <= endDate))).ToList();

				var managerViewModel = new ManagerViewModelFiltering
				{
					Manager = manager.Data,
					FilteredCalls = filteredCalls
				};
				return View(managerViewModel);
			}
			else
			{
				TempData["SuccessMessage"] = "Произошла ошибка загрузки данных, проверьте наличие зарегистрированных звонков";
				return View("ErrorCalls", new { managerId = id });
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetManagerInfoMeetings(int id, string? startDateRange, string? endDateRange)
		{
			var manager = await _managerService.Get(id);

			DateTime? startDate = string.IsNullOrEmpty(startDateRange) ? null : DateTime.Parse(startDateRange);
			DateTime? endDate = string.IsNullOrEmpty(endDateRange) ? null : DateTime.Parse(endDateRange);

			if (manager.StatusCode == Domain.Enum.StatusCode.OK)
			{
				List<Meeting> filteredMeetings = (manager.Data.Meetings.Where(meeting
					=> (!startDate.HasValue || meeting.Date >= startDate)
					&& (!endDate.HasValue || meeting.Date <= endDate))).ToList();

				var managerViewModel = new ManagerViewModelFiltering
				{
					Manager = manager.Data,
					FilteredMeetings = filteredMeetings
				};
				return View(managerViewModel);
			}
			else
			{
				TempData["SuccessMessage"] = "Произошла ошибка загрузки данных, проверьте наличие зарегистрированных звонков";
				return View("ErrorMeetings", new { managerId = id });
			}
		}
		[HttpGet]
		public async Task<IActionResult> GetManagerInfoById(int id)
		{
			var manager = await _managerService.Get(id);
			if (manager.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(manager.Data);
			}
			else
			{
				return View("Error");
			}
		}

		[HttpGet]
		public async Task<IActionResult> CreateManager(int? id)
		{
			var NewManager = new Manager();

			return View(NewManager);
		}

		[HttpPost]
		public async Task<IActionResult> UpsertManager(Manager model, string password)
		{
			var identityResult = await _userManager.FindByEmailAsync(model.Email);

			if (ModelState.IsValid)
			{
                if (model.ManagerId == 0 && identityResult == null)
				{
					var response = await _headDepartmentService.GetByDepartmentNumber(model.DepartmentNumber);
					model.HeadDepartmentId = response.Data.HeadDepartmentId;
					await _managerService.Insert(model);
				}
				else if(model.ManagerId != 0 && identityResult != null)
				{
					await _managerService.Update(model.ManagerId, model);
				}
				else
				{
					return RedirectToAction("Error");
				}

                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
					ManagerId = model.ManagerId,
                };

                IdentityResult result;
                if (model.ManagerId == 0)
                {
                    result = await _userManager.CreateAsync(user, password);
                }
                else
                {
                    var existingUser = await _userManager.FindByEmailAsync(model.Email);
                    if (existingUser != null)
                    {
                        result = await _userManager.UpdateAsync(existingUser);
                    }
                    else
                    {
                        result = await _userManager.CreateAsync(user, password);
                    }
                }
                if (!await _userManager.IsInRoleAsync(user, "manager"))
                {
                    await _userManager.AddToRoleAsync(user, "manager");
                }
            }
			else
			{
				return RedirectToAction("Error");
			}
			return RedirectToAction("GetManagersDepartment", "HeadDepartment", new { departmentNumber = model.DepartmentNumber});
		}

		[HttpGet]
		public async Task<IActionResult> ChangingDataManager(int id)
		{
			var response = await _managerService.Get(id);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			return RedirectToAction("Error");
		}

		[HttpGet]
		public async Task<IActionResult> DeleteManager(int id, int _departmentNumber)
		{
			var response = await _managerService.Delete(id);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return RedirectToAction("GetManagersDepartment", "HeadDepartment", new { departmentNumber = _departmentNumber });
			}
			else
			{
				return RedirectToAction("Error");
			}
		}
	}
}
