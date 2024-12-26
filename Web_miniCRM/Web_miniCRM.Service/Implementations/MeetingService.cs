using Web_miniCRM.DAL.Interfaces;
using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Domain.Enum;
using Web_miniCRM.Domain.Response;
using Web_miniCRM.Service.Interfaces;

namespace Web_miniCRM.Service.Implementations
{
	public class MeetingService : IMeetingService
	{
		private readonly IMeetingRepository _meetingRepository;

		public MeetingService(IMeetingRepository meetingRepository)
		{
			_meetingRepository = meetingRepository;
		}

		public async Task<IBaseResponse<bool>> Delete(int id)
		{
			var baseRepository = new BaseResponse<bool>();
			try
			{
				var meeting = await _meetingRepository.Get(id);
				if (meeting == null)
				{
					baseRepository.StatusCode = StatusCode.InternalServerError;
					baseRepository.Description = "Элемент не найден";
					baseRepository.Data = false;
				}
				else
				{
					await _meetingRepository.Delete(meeting);
					baseRepository.StatusCode = StatusCode.OK;
					baseRepository.Description = "Элемент удален";
					baseRepository.Data = true;
				}
				return baseRepository;
				
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

		public async Task<IBaseResponse<List<Meeting>>> GetByManagerId(int id)
		{
			var baseResponse = new BaseResponse<List<Meeting>>();
			try
			{
				var meetings = await _meetingRepository.GetByManagerId(id);
				if (meetings.Count == 0)
				{
					baseResponse.StatusCode = StatusCode.InternalServerError;
					baseResponse.Description = "Найдено 0 элементов";
					return baseResponse;
				}
				baseResponse.StatusCode = StatusCode.OK;
				baseResponse.Data = meetings;
				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<Meeting>>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"[GetByManagerId] : {ex.Message}"
				};
			}
		}

		public async Task<IBaseResponse<Meeting>> GetMeetingId(int id)
		{
			var baseResponse = new BaseResponse<Meeting>();
			try
			{
				var meeting = await _meetingRepository.Get(id);
				if(meeting == null)
				{
					baseResponse.StatusCode = StatusCode.InternalServerError;
					baseResponse.Description = "Элемент не найден";
					return baseResponse;
				}
				baseResponse.StatusCode = StatusCode.OK;
				baseResponse.Data = meeting;
				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<Meeting>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"[GetMeetingId] : {ex.Message}"
				};
			}
		}

		public async Task<IBaseResponse<List<Meeting>>> GetMeetings()
		{
			var baseResponse = new BaseResponse<List<Meeting>>();
			try
			{
				var meetings = await _meetingRepository.GetAll();
				if(meetings.Count == 0)
				{
					baseResponse.StatusCode = StatusCode.InternalServerError;
					baseResponse.Description = "Найдено 0 элементов";
					return baseResponse;
				}
				baseResponse.StatusCode = StatusCode.OK;
				baseResponse.Data = meetings;
				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<Meeting>>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"[GetMeetings] : {ex.Message}"
				};
			}
		}

		public async Task<IBaseResponse<bool>> Insert(Meeting meeting)
		{
			var baseResponse = new BaseResponse<bool>();
			try
			{
				await _meetingRepository.Insert(meeting);
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

		public async Task<IBaseResponse<Meeting>> Update(int id, Meeting NewMeeting)
		{
			var baseResponse = new BaseResponse<Meeting>();
			try
			{
				var meeting = await _meetingRepository.Get(id);
				if (meeting == null)
				{
					baseResponse.StatusCode = StatusCode.InternalServerError;
					baseResponse.Description = "Элемент не найден";
					return baseResponse;
				}
				else
				{
					meeting.NameContactPerson = NewMeeting.NameContactPerson;
					meeting.Date = NewMeeting.Date;
					meeting.Address = NewMeeting.Address;
					meeting.Details = NewMeeting.Details;
					await _meetingRepository.Update(meeting);

					baseResponse.StatusCode = StatusCode.OK;
				}
				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<Meeting>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"[Update] : {ex.Message}"
				};
			}
		}
	}
}
