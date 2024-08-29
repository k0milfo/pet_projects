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
            if(headDepartments.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(headDepartments.Data);
            }
            return View("Error");
        }
        [HttpGet]
        public async Task<IActionResult> GetByDepartmentId(int id)
        {
            var manager = await _headDepartmentService.Get(id);
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
		public async Task<IActionResult> GetCompanyDepartment(int departmentNumber)
		{
            var response = await _headDepartmentService.GetByDepartmentNumber(departmentNumber);
            var responseData = response.Data;
            var companies = responseData.Managers.SelectMany(i => i.Companies).ToList();
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(companies);
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
	}
}
