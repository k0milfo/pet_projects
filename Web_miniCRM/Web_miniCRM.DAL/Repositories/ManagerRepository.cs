using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web_miniCRM.DAL.Interfaces;
using Web_miniCRM.Domain.Entity;

namespace Web_miniCRM.DAL.Repositories
{
	public class ManagerRepository : IManagerRepository
	{
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;

		public ManagerRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
		{
			_db = dbContext;
			_userManager = userManager;
		}
		public async Task<bool> Delete(Manager entity)
		{
			var manager = await _userManager.FindByEmailAsync(entity.Email);
			var identityResult = await _userManager.DeleteAsync(manager);
			_db.Remove(entity);
			await _db.SaveChangesAsync();
			return true;
		}

		public async Task<Manager> Get(int id)
		{
			return await _db.Managers
				.Include(i => i.Companies)
					.ThenInclude(c => c.Invoices)
					.ThenInclude(d => d.InvoiceItems)
					.ThenInclude(j => j.Product)
				.Include(ii => ii.Meetings)
				.Include(iii => iii.Calls)
				.Include(i => i.HeadDepartment)
				.FirstOrDefaultAsync(iiii => iiii.ManagerId == id);
		}

        public async Task<Manager> GetByEmail(string email)
        {
            return await _db.Managers.FirstOrDefaultAsync(item => item.Email.Equals(email));
        }

        public async Task<List<Manager>> GetAll()
		{
			return await _db.Managers.ToListAsync();
		}

		public async Task<List<Manager>> GetManagersByDepartmentId(int id)
		{
			return await _db.Managers.Where(i => i.HeadDepartmentId == id)
				.Include(i => i.Companies)
				.Include(i => i.Calls)
				.Include(i => i.Invoices)
				.Include(i => i.Meetings)
				.ToListAsync();
		}

		public async Task<bool> Insert(Manager entity)
		{
			_db.AddAsync(entity);
			await _db.SaveChangesAsync();
			return true;
		}

		public async Task<Manager> Update(Manager entity)
		{
			_db.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
