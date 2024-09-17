using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_miniCRM.Domain.Entity;

public class AccountController : Controller
{
	private readonly SignInManager<ApplicationUser> _signInManager;
	private readonly UserManager<ApplicationUser> _userManager;

	public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
	{
		_signInManager = signInManager;
		_userManager = userManager;
	}

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel? model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (await _userManager.IsInRoleAsync(user, "admin"))
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (await _userManager.IsInRoleAsync(user, "headdepartment"))
                {
                    return RedirectToAction("GetByDepartmentId", "HeadDepartment", new {id = user.HeadDepartmentId});
                }
                else if (await _userManager.IsInRoleAsync(user, "manager"))
                {
                    return RedirectToAction("GetManagerInfoById", "Manager", new {id = user.ManagerId});
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Неверное имя пользователя или пароль.");
            }
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
	{
		await _signInManager.SignOutAsync();
		return RedirectToAction("Login", "Account");
	}
    [HttpGet]
    public async Task<IActionResult> CreateNewPassword(string email, string firstname, string lastname)
    {
		ViewBag.Email = email;
		ViewBag.FirstName = firstname;
		ViewBag.LastName = lastname;

		return View();
    }

	[HttpPost]
    public async Task<IActionResult> ResetPassword(string email, string password)
    {
		var user = await _userManager.FindByEmailAsync(email);
		if (user == null)
		{
			return RedirectToAction("Error");
		}

		var result = await _userManager.RemovePasswordAsync(user);
		if (!result.Succeeded)
		{
			return RedirectToAction("Error");
		}

		result = await _userManager.AddPasswordAsync(user, password);
		if (!result.Succeeded)
		{
			return RedirectToAction("Error");
		}

        return user.ManagerId == null ? RedirectToAction("GetHeadDepartments", "HeadDepartment") : RedirectToAction("GetManagers", "Manager");
	}
}