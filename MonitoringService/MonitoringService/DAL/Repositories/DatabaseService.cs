using Microsoft.EntityFrameworkCore;
using Monitoring_Service.DAL.Interfaces;
using Monitoring_Service.Domain.Entity;

namespace Monitoring_Service.DAL.Repositories
{
	public class DatabaseService : IDatabaseService<SensorData>
	{
		private readonly ApplicationDbContext _db;

		public DatabaseService(ApplicationDbContext db)
		{
			_db = db;
		}

		public async Task<SensorData> Get(int id)
		{
			return await _db.SensorData
				.FirstOrDefaultAsync(i => i.Id == id);
		}

		public async Task<bool> Delete(int id)
		{
			var entity = await _db.SensorData.FindAsync(id);
			if (entity == null) return false;

			_db.SensorData.Remove(entity);
			await _db.SaveChangesAsync();
			return true;
		}

		public async Task<List<SensorData>> GetAll()
		{
			return await _db.SensorData.ToListAsync();
		}

		public async Task<bool> Insert(SensorData entity)
		{
			await _db.SensorData.AddAsync(entity);
			await _db.SaveChangesAsync();
			return true;
		}
	}
}
