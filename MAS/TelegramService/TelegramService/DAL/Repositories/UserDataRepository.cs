using Microsoft.EntityFrameworkCore;
using TelegramService.DAL.Interfaces;
using TelegramService.Domain.Entity;

namespace TelegramService.DAL.Repositories
{
	public class UserDataRepository : IUserDataRepository
	{
		private readonly ApplicationDbContext _db;

		public UserDataRepository(ApplicationDbContext db)
		{
			_db = db;
		}
		public async Task<User> GetDeviceId(string deviceId)
		{
			return await _db.UserData.FirstOrDefaultAsync(i => i.DeviceId == deviceId);
		}
		public async Task<User> GetChatId(long id)
		{
			return await _db.UserData.FirstOrDefaultAsync(i => i.ChatId == id);
		}
		public async Task<bool> IsRegistreDevice(string DeviceId)
		{
			return await _db.UserData.AnyAsync(i => i.DeviceId == DeviceId);
		}

		public async Task<bool> Insert(User entity)
		{
			await _db.UserData.AddAsync(entity);
			await _db.SaveChangesAsync();
			return true;
		}
	}
}
