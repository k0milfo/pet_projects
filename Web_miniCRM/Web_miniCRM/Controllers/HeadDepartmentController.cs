using Microsoft.AspNetCore.Mvc;
using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Service.Implementations;
using Web_miniCRM.Service.Interfaces;

namespace Web_miniCRM.Controllers
{
    public class HeadDepartmentController : Controller
    {
        private readonly IHeadDepartmentService _headDepartmentService;

        public HeadDepartmentController(IHeadDepartmentService headDepartmentService)
        {
            _headDepartmentService = headDepartmentService;
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
    }
}
