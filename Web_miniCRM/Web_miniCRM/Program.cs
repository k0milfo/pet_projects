using Microsoft.EntityFrameworkCore;
using Web_miniCRM.DAL;
using Web_miniCRM.DAL.Interfaces;
using Web_miniCRM.DAL.Repositories;
using Web_miniCRM.Service.Interfaces;
using Web_miniCRM.Service.Implementations;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Покдлючение к базе данных
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer(connection));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
	.AddRoles<IdentityRole>();

builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireDigit = false;
	options.Password.RequiredLength = 5;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireLowercase = false;
});



builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
builder.Services.AddScoped<IManagerService, ManagerService>();

builder.Services.AddScoped<ICallRepository, CallRepository>();
builder.Services.AddScoped<ICallService, CallService>();

builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();
builder.Services.AddScoped<IMeetingService, MeetingService>();

builder.Services.AddScoped<IHeadDepartmentRepository, HeadDepartmentRepository>();
builder.Services.AddScoped<IHeadDepartmentService, HeadDepartmentService>();



var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
	var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

	// Создаем роли, если они не существуют
	string[] roleNames = { "admin", "headdepartment", "manager" };
	foreach (var roleName in roleNames)
	{
		var roleExist = await roleManager.RoleExistsAsync(roleName);
		if (!roleExist)
		{
			await roleManager.CreateAsync(new IdentityRole(roleName));
		}
	}

	// Создаем пользователя admin и присваиваем ему роль admin
	var adminUser = await userManager.FindByEmailAsync("admin");
	if (adminUser == null)
	{
		var user = new ApplicationUser
		{
			UserName = "admin", //admin@example.com
            Email = "admin@example.com"
		};
		var createAdmin = await userManager.CreateAsync(user, "admin"); //AdminPassword123
        if (createAdmin.Succeeded)
		{
			await userManager.AddToRoleAsync(user, "admin");
		}
	}
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
//pattern: "{controller=Home}/{action=Index}/{id?}");
pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
