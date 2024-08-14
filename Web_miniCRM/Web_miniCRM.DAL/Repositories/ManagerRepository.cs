using Microsoft.EntityFrameworkCore;
using Web_miniCRM.DAL.Interfaces;
using Web_miniCRM.Domain.Entity;

namespace Web_miniCRM.DAL.Repositories
{
	public class ManagerRepository : IManagerRepository
	{
		private readonly ApplicationDbContext _db;

		public ManagerRepository(ApplicationDbContext dbContext)
		{
			_db = dbContext;
		}
		public async Task<bool> Delete(Manager entity)
		{
			_db.Remove(entity);
			_db.SaveChanges();
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
				.FirstOrDefaultAsync(iiii => iiii.ManagerId == id);
		}

		public async Task<List<Manager>> GetAll()
		{
			return await _db.Managers.ToListAsync();
		}

		public async Task<bool> Insert(Manager entity)
		{
			_db.Add(entity);
			_db.SaveChangesAsync();
			return true;
		}

		public async Task<Manager> Update(Manager entity)
		{
			_db.Update(entity);
			_db.SaveChangesAsync();
			return entity;
		}
	}
}
