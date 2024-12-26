using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web_miniCRM.DAL.Interfaces;
using Web_miniCRM.Domain.Entity;

namespace Web_miniCRM.DAL.Repositories
{
	public class HeadDepartmentRepository : IHeadDepartmentRepository
	{
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		public HeadDepartmentRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
		{
			_db = dbContext;
			_userManager = userManager;
		}

		public async Task<bool> Delete(HeadDepartment entity)
		{
			var manager = await _userManager.FindByEmailAsync(entity.Email);
			var identityResult = await _userManager.DeleteAsync(manager);
			_db.Remove(entity);
			await _db.SaveChangesAsync();
			return true;
		}

		public async Task<HeadDepartment> GetByEmail(string email)
		{
			return await _db.HeadDepartments.FirstOrDefaultAsync(item => item.Email.Equals(email));
		}

		public async Task<HeadDepartment> Get(int id)
		{
			//return await _db.HeadDepartments.FirstOrDefaultAsync(i => i.HeadDepartmentId == id);

			return await _db.HeadDepartments
	.Include(i => i.Managers)
		.ThenInclude(i => i.Invoices)
			.ThenInclude(i => i.InvoiceItems)
				.ThenInclude(i => i.Product)
	.Include(i => i.Managers)
		.ThenInclude(i => i.Meetings)
	.Include(i => i.Managers)
		.ThenInclude(i => i.Calls)
	.Include(i => i.Managers)
		.ThenInclude(i => i.Companies)
	.FirstOrDefaultAsync(i => i.HeadDepartmentId == id);
		}

		public async Task<List<HeadDepartment>> GetAll()
		{
			return await _db.HeadDepartments.ToListAsync();
		}

		public async Task<HeadDepartment> GetByDepartmentNumber(int DepartmentNumber)
		{
			return await _db.HeadDepartments
	.Include(i => i.Managers)
		.ThenInclude(i => i.Invoices)
			.ThenInclude(i => i.InvoiceItems)
				.ThenInclude(i => i.Product)
	.Include(i => i.Managers)
		.ThenInclude(i => i.Meetings)
	.Include(i => i.Managers)
		.ThenInclude(i => i.Calls)
	.Include(i => i.Managers)
		.ThenInclude(i => i.Companies)
				.FirstOrDefaultAsync(i => i.DepartmentNumber == DepartmentNumber);
		}
		public async Task<HeadDepartment> GetNotFullByNumber(int number)
		{
			//return await _db.HeadDepartments.FirstOrDefaultAsync(i => i.HeadDepartmentId == id);

			return await _db.HeadDepartments
	.Include(i => i.Managers)
	.FirstOrDefaultAsync(i => i.DepartmentNumber == number);
		}

		public async Task<bool> Insert(HeadDepartment entity)
		{
			_db.AddAsync(entity);
			await _db.SaveChangesAsync();
			return true;
		}

		public async Task<HeadDepartment> Update(HeadDepartment entity)
		{
			_db.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
