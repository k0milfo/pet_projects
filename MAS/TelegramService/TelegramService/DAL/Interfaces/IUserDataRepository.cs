using TelegramService.Domain.Entity;

namespace TelegramService.DAL.Interfaces
{
	public interface IUserDataRepository
	{
		Task<User> GetDeviceId(string id);
		Task<bool> Insert(User entity);
		Task<User> GetChatId(long id);
		Task<bool> IsRegistreDevice(string deviceId);
	}
}
