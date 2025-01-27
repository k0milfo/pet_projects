﻿using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        public HeadDepartmentController(IHeadDepartmentService headDepartmentService, IManagerService managerService, UserManager<ApplicationUser> userManager)
		{
			_headDepartmentService = headDepartmentService;
			_managerService = managerService;
            _userManager = userManager;

        }

		[HttpGet]
		public async Task<IActionResult> CreateHeadDepartment(int? id)
		{
			var NewHeadDepartment = new HeadDepartment();

			return View(NewHeadDepartment);
		}

		[HttpPost]
		public async Task<IActionResult> UpsertHeadDepartment(HeadDepartment model, string? password)
		{
			var identityResult = await _userManager.FindByEmailAsync(model.Email);
			if (ModelState.IsValid)
			{
				if (model.HeadDepartmentId == 0 && identityResult == null)
				{
					await _headDepartmentService.Insert(model);
				}
				else if(model.HeadDepartmentId != 0 && identityResult != null)
				{
					await _headDepartmentService.Update(model.HeadDepartmentId, model);
				}
				else
				{
					return RedirectToAction("Error");
				}

                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    HeadDepartmentId = model.HeadDepartmentId,
                };

                IdentityResult result;
                if (model.HeadDepartmentId == 0)
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
                if (!await _userManager.IsInRoleAsync(user, "headdepartment"))
                {
                    await _userManager.AddToRoleAsync(user, "headdepartment");
                }

            }
			else
			{
				return RedirectToAction("Error");
			}
			return RedirectToAction("GetHeadDepartments", "HeadDepartment");
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

		[HttpGet]
		public async Task<IActionResult> ChangingDataHeadDepartment(string email)
		{
			var response = await _headDepartmentService.GetByEmail(email);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			return RedirectToAction("Error");
		}

		[HttpGet]
		public async Task<IActionResult> DeleteHeadDepartment(string email, int _departmentNumber)
		{
			var response = await _headDepartmentService.DeleteByEmail(email);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return RedirectToAction("GetHeadDepartments", "HeadDepartment");
			}
			else
			{
				return RedirectToAction("Error");
			}
		}
	}
}
