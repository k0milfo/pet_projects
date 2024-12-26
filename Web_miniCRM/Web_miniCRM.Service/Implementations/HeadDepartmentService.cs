using Web_miniCRM.DAL.Interfaces;
using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Domain.Enum;
using Web_miniCRM.Domain.Response;
using Web_miniCRM.Service.Interfaces;

namespace Web_miniCRM.Service.Implementations
{
    public class HeadDepartmentService : IHeadDepartmentService
    {
        private readonly IHeadDepartmentRepository _headDepartmentRepository;

        public HeadDepartmentService(IHeadDepartmentRepository headDepartmentRepository)
        {
            _headDepartmentRepository = headDepartmentRepository;
        }

        public async Task<IBaseResponse<bool>> DeleteByEmail(string email)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
				var headDepartmentRepository = await _headDepartmentRepository.GetByEmail(email);
				if (headDepartmentRepository == null)
                {
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    baseResponse.Description = "Элемент не найден";
                    baseResponse.Data = false;
                }
                else
                {
                    await _headDepartmentRepository.Delete(headDepartmentRepository);
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

        public async Task<IBaseResponse<HeadDepartment>> Get(int id)
        {
            var baseResponse = new BaseResponse<HeadDepartment>();
            try
            {
                var headDepartmentRepository = await _headDepartmentRepository.Get(id);
                if (headDepartmentRepository == null)
                {
                    baseResponse.Description = $"Элемент не найден";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }
                baseResponse.Data = headDepartmentRepository;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<HeadDepartment>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"[Get] : {ex.Message}"
                };
            }
        }

		public async Task<IBaseResponse<HeadDepartment>> GetByEmail(string email)
		{
			var baseResponse = new BaseResponse<HeadDepartment>();
			try
			{
				var headDepartmentRepository = await _headDepartmentRepository.GetByEmail(email);
				if (headDepartmentRepository == null)
				{
					baseResponse.Description = $"Элемент не найден";
					baseResponse.StatusCode = StatusCode.InternalServerError;
					return baseResponse;
				}
				baseResponse.Data = headDepartmentRepository;
				baseResponse.StatusCode = StatusCode.OK;
				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<HeadDepartment>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"[GetByEmail] : {ex.Message}"
				};
			}
		}

		public async Task<IBaseResponse<HeadDepartment>> GetByDepartmentNumber(int DepartmentNumber)
		{
			var baseResponse = new BaseResponse<HeadDepartment>();
			try
			{
				var headDepartmentRepository = await _headDepartmentRepository.GetByDepartmentNumber(DepartmentNumber);
				if (headDepartmentRepository == null)
				{
					baseResponse.Description = $"Элемент не найден";
					baseResponse.StatusCode = StatusCode.InternalServerError;
					return baseResponse;
				}
				baseResponse.Data = headDepartmentRepository;
				baseResponse.StatusCode = StatusCode.OK;
				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<HeadDepartment>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"[GetByDepartmentNumber] : {ex.Message}"
				};
			}
		}

		public async Task<IBaseResponse<List<HeadDepartment>>> GetHeadDepartments()
        {
            var baseResponse = new BaseResponse<List<HeadDepartment>>();

            try
            {
                var headDepartmentRepository = await _headDepartmentRepository.GetAll();
                if (headDepartmentRepository.Count == 0)
                {
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    baseResponse.Description = "Найдено 0 элементов";
                    return baseResponse;
                }
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = headDepartmentRepository;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<HeadDepartment>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"[GetAll] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> Insert(HeadDepartment NewHeadDepartment)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                await _headDepartmentRepository.Insert(NewHeadDepartment);
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

        public async Task<IBaseResponse<HeadDepartment>> Update(int id, HeadDepartment _headDepartment)
        {
            var baseResponse = new BaseResponse<HeadDepartment>();
            try
            {
                var headDepartmentRepository = await _headDepartmentRepository.Get(id);
                if (headDepartmentRepository == null)
                {
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    baseResponse.Description = "Элемент не найден";
                    return baseResponse;
                }
                else
                {
					headDepartmentRepository.Managers = _headDepartment.Managers;
					headDepartmentRepository.DepartmentNumber = _headDepartment.DepartmentNumber;
					headDepartmentRepository.FirstName = _headDepartment.FirstName;
					headDepartmentRepository.LastName = _headDepartment.LastName;
					headDepartmentRepository.Email = _headDepartment.Email;
					headDepartmentRepository.NumberPhone = _headDepartment.NumberPhone;
                    await _headDepartmentRepository.Update(headDepartmentRepository);
                    baseResponse.StatusCode = StatusCode.OK;

                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<HeadDepartment>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"[Update] : {ex.Message}"
                };
            }
        }

		public async Task<IBaseResponse<bool>> DeleteById(int id)
		{
			var baseResponse = new BaseResponse<bool>();
			try
			{
                var headDepartmentRepository = await _headDepartmentRepository.Get(id);
                if (headDepartmentRepository == null)
				{
					baseResponse.StatusCode = StatusCode.InternalServerError;
					baseResponse.Description = "Элемент не найден";
					baseResponse.Data = false;
				}
				else
				{
					await _headDepartmentRepository.Delete(headDepartmentRepository);
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

	}
}
