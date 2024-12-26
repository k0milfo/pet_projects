using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Web_miniCRM.DAL.Interfaces;
using Web_miniCRM.Domain.Entity;

namespace Web_miniCRM.DAL.Repositories
{
	public class MeetingRepository : IMeetingRepository
	{
		private readonly ApplicationDbContext _db;

		public MeetingRepository(ApplicationDbContext dbContext)
		{
			_db = dbContext;
		}

		public async Task<bool> Delete(Meeting entity)
		{
			_db.Remove(entity);
			await _db.SaveChangesAsync();
			return true;
		}

		public async Task<Meeting> Get(int id)
		{
			return await _db.Meetings.FirstOrDefaultAsync(i => i.MeetingId == id);
		}

		public async Task<List<Meeting>> GetAll()
		{
			return await _db.Meetings.ToListAsync();
		}

		public async Task<List<Meeting>> GetByManagerId(int id)
		{
			return await _db.Meetings
				.Include(c => c.Company)
				.Where(i => i.ManagerId == id)
				.ToListAsync();
		}

		public async Task<bool> Insert(Meeting entity)
		{
			_db.Add(entity);
			_db.SaveChangesAsync();
			return true;
		}

		public async Task<Meeting> Update(Meeting entity)
		{
			_db.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
