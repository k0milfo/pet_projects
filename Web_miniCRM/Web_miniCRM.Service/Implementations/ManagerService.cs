using Web_miniCRM.DAL.Interfaces;
using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Domain.Enum;
using Web_miniCRM.Domain.Response;
using Web_miniCRM.Service.Interfaces;

namespace Web_miniCRM.Service.Implementations
{
	public class ManagerService : IManagerService
	{
		private readonly IManagerRepository _managerRepository;
		private readonly IHeadDepartmentRepository _headDepartmentRepository;

		public ManagerService(IManagerRepository managerRepository, IHeadDepartmentRepository headDepartmentRepository)
		{
			_managerRepository = managerRepository;
			_headDepartmentRepository = headDepartmentRepository;
		}

		public async Task<IBaseResponse<bool>> Delete(int id)
		{
			var baseResponse = new BaseResponse<bool>();
			try
			{
				var manager = await _managerRepository.Get(id);
				if(manager == null)
				{
					baseResponse.StatusCode = StatusCode.InternalServerError;
					baseResponse.Description = "Элемент не найден";
					baseResponse.Data = false;
				}
				else
				{
					await _managerRepository.Delete(manager);
					baseResponse.StatusCode = StatusCode.OK;
					baseResponse.Description = "Элемент удален";
					baseResponse.Data = true;
				}

				return baseResponse;
			}
			catch (Exception ex) 
			{
				return new BaseResponse<bool>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"[Delete] : {ex.Message}"
				};
			}
		}

		public async Task<IBaseResponse<Manager>> Get(int id)
		{
			var baseResponse = new BaseResponse<Manager>();
			try
			{
				var manager = await _managerRepository.Get(id);
				if(manager == null)
				{
					baseResponse.Description = $"Элемент не найден";
					baseResponse.StatusCode = StatusCode.InternalServerError;
					return baseResponse;
				}
				baseResponse.Data = manager;
				baseResponse.StatusCode = StatusCode.OK;
				return baseResponse;
			}
			catch (Exception ex) 
			{
				return new BaseResponse<Manager>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"[Get] : {ex.Message}"
				};
			}
		}

		public async Task<IBaseResponse<List<Manager>>> GetManagers()
		{
			var baseResponse = new BaseResponse<List<Manager>>();

			try
			{
				var managers = await _managerRepository.GetAll();
				if(managers.Count == 0)
				{
					baseResponse.StatusCode = StatusCode.InternalServerError;
					baseResponse.Description = "Найдено 0 элементов";
					return baseResponse;
				}
				baseResponse.StatusCode = StatusCode.OK;
				baseResponse.Data = managers;
				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<Manager>>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"[GetAll] : {ex.Message}"
				};
			}
		}

		public async Task<IBaseResponse<List<Manager>>> GetManagersByDepartmentId(int id)
		{
			var baseResponse = new BaseResponse<List<Manager>>();

			try
			{
				var managers = await _managerRepository.GetManagersByDepartmentId(id);
				if (managers.Count == 0)
				{
					baseResponse.StatusCode = StatusCode.InternalServerError;
					baseResponse.Description = "Найдено 0 элементов";
					return baseResponse;
				}
				baseResponse.StatusCode = StatusCode.OK;
				baseResponse.Data = managers;
				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<Manager>>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"[GetAll] : {ex.Message}"
				};
			}
		}

        public async Task<IBaseResponse<Manager>> GetManagerByEmail(string email)
        {
            var baseResponse = new BaseResponse<Manager>();
            try
            {
                var manager = await _managerRepository.GetByEmail(email);
                if (manager == null)
                {
                    baseResponse.Description = $"Элемент не найден";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }
                baseResponse.Data = manager;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Manager>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"[GetByEmail] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> Insert(Manager NewManager)
		{
			var baseResponse = new BaseResponse<bool>();
			try
			{
				await _managerRepository.Insert(NewManager);
				baseResponse.StatusCode = StatusCode.OK;
				baseResponse.Data = true;
				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"[Insert] : {ex.Message}",
					Data = false
				};
			}
		}

		public async Task<IBaseResponse<Manager>> Update(int id, Manager NewManager)
		{
			var baseResponse = new BaseResponse<Manager>();
			try
			{
				var manager = await _managerRepository.Get(id);
				if(manager == null)
				{
					baseResponse.StatusCode= StatusCode.InternalServerError;
					baseResponse.Description = "Элемент не найден";
					return baseResponse;
				}
				else
				{
					var response = await _headDepartmentRepository.GetNotFullByNumber(NewManager.DepartmentNumber);

					manager.DepartmentNumber = NewManager.DepartmentNumber;
					manager.HeadDepartmentId = response.HeadDepartmentId;
					manager.FirstName = NewManager.FirstName;
					manager.LastName = NewManager.LastName;
					manager.Email = NewManager.Email;
					manager.NumberPhone = NewManager.NumberPhone;
					manager.Calls = NewManager.Calls;
					manager.Companies = NewManager.Companies;
					manager.Meetings = NewManager.Meetings;
					await _managerRepository.Update(manager);
					baseResponse.StatusCode = StatusCode.OK;
					
					return baseResponse;
				}
				return baseResponse;
			}
			catch(Exception ex)
			{
				return new BaseResponse<Manager>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"[Update] : {ex.Message}"
				};
			}
		}
	}
}
