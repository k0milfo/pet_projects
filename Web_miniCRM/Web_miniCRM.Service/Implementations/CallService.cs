using Web_miniCRM.DAL.Interfaces;
using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Domain.Enum;
using Web_miniCRM.Domain.Response;
using Web_miniCRM.Service.Interfaces;

namespace Web_miniCRM.Service.Implementations
{
	public class CallService : ICallService
	{
		private readonly ICallRepository _callRepository;

		public CallService(ICallRepository callRepository)
		{
			_callRepository = callRepository;
		}

		public async Task<IBaseResponse<bool>> Delete(int id)
		{
			var baseResponse = new BaseResponse<bool>();
			try
			{
				var call = await _callRepository.Get(id);
				if (call == null)
				{
					baseResponse.Data = false;
					baseResponse.StatusCode = StatusCode.InternalServerError;
					baseResponse.Description = "Элемент не найден";
				}
				else
				{
					_callRepository.Delete(call);
					baseResponse.Data = true;
					baseResponse.StatusCode = StatusCode.OK;
					baseResponse.Description = "Элемент удален";
				}
				return baseResponse;
			}
			catch (Exception ex) 
			{
				return new BaseResponse<bool>()
				{
					Description = $"[Delete] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
		}

		public async Task<IBaseResponse<Call>> GetCallById(int id)
		{
			var baseResponse = new BaseResponse<Call>();

			try
			{
				var call = await _callRepository.Get(id);
				if(call == null)
				{
					baseResponse.StatusCode = StatusCode.InternalServerError;
					baseResponse.Description = "Элемент не найден";

					return baseResponse;
				}
				baseResponse.Data = call;
				baseResponse.StatusCode = StatusCode.OK;
				return baseResponse;
			}
			catch (Exception ex) 
			{
				return new BaseResponse<Call>()
				{
					Description = $"GetCallById : {ex.Message}"
				};
			}
		}
		public async Task<IBaseResponse<List<Call>>> GetByManagerId(int id)
		{
			var baseResponse = new BaseResponse<List<Call>>();

			try
			{
				var calls = await _callRepository.GetByManagerId(id);
				if (calls == null)
				{
					baseResponse.StatusCode = StatusCode.InternalServerError;
					baseResponse.Description = "Элемент не найден";

					return baseResponse;
				}
				baseResponse.Data = calls;
				baseResponse.StatusCode = StatusCode.OK;
				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<Call>>()
				{
					Description = $"GetCallById : {ex.Message}"
				};
			}
		}
		public async Task<IBaseResponse<List<Call>>> GetCalls()
		{
			var baseRepository = new BaseResponse<List<Call>>();

			try
			{
				var calls = await _callRepository.GetAll();
				if(calls.Count == 0)
				{
					baseRepository.StatusCode = StatusCode.InternalServerError;
					baseRepository.Description = "Найдено 0 элементов";
				}
				baseRepository.Data = calls;
				baseRepository.StatusCode = StatusCode.OK;
				return baseRepository;
			}
			catch (Exception ex) 
			{
				return new BaseResponse<List<Call>>()
				{
					Description = $"[GetCalls] : {ex.Message}"
				};
			}
		}

		public async Task<IBaseResponse<bool>> Insert(Call call)
		{
			var baseResponse = new BaseResponse<bool>();
			try
			{
				await _callRepository.Insert(call);

				baseResponse.Data = true;
				return baseResponse;
			}
			catch (Exception ex) 
			{
				return new BaseResponse<bool>
				{
					Description = $"[Insert] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
		}

		public async Task<IBaseResponse<Call>> Update(int id, Call NewCall)
		{
			var baseResponse = new BaseResponse<Call>();
			try
			{
				var call = await _callRepository.Get(id);
				if(call == null)
				{
					baseResponse.Description = "Элемент не найден";
					baseResponse.StatusCode = StatusCode.InternalServerError;
					return baseResponse;
				}
				call.NameContactPerson = NewCall.NameContactPerson;
				call.Details = NewCall.Details;
				call.Date = NewCall.Date;
				call.PhoneNumber = NewCall.PhoneNumber;
				await _callRepository.Update(call);

				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<Call>()
				{
					Description = $"[Update] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
		}
	}
}
