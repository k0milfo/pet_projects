using Microsoft.EntityFrameworkCore;
using Web_miniCRM.DAL.Interfaces;
using Web_miniCRM.Domain.Entity;

namespace Web_miniCRM.DAL.Repositories
{
	public class CallRepository : ICallRepository
	{
		private readonly ApplicationDbContext _db;

		public CallRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		public async Task<bool> Delete(Call entity)
		{
			_db.Remove(entity);
			await _db.SaveChangesAsync();
			return true;
		}

		public async Task<Call> Get(int id)
		{
			return await _db.Calls
				.FirstOrDefaultAsync(i => i.CallId == id);
		}
		public async Task<List<Call>> GetByManagerId(int id)
		{
			return await _db.Calls
				.Include(c => c.Company)
				.Where(i => i.ManagerId == id).ToListAsync();
		}

		public async Task<List<Call>> GetAll()
		{
			return await _db.Calls.ToListAsync();
		}

		public async Task<bool> Insert(Call entity)
		{
			_db.Add(entity);
			await _db.SaveChangesAsync();
			return true;
		}

		public async Task<Call> Update(Call entity)
		{
			_db.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
