using Web_miniCRM.DAL.Interfaces;
using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Domain.Enum;
using Web_miniCRM.Domain.Response;
using Web_miniCRM.Service.Interfaces;

namespace Web_miniCRM.Service.Implementations
{
    public class InvoiceService : IInvoiceService
    {

        private readonly IInvoiceRepository _invoiceRepository;
        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<IBaseResponse<List<Invoice>>> GetInvoices()
        {
            var baseResponse = new BaseResponse<List<Invoice>>();
            try
            {
                var invoices = await _invoiceRepository.GetAll();
                if (invoices.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                baseResponse.Data = invoices;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Invoice>>()
                {
                    Description = $"[GetInvoices] : {ex.Message}"
                };
            }
        }
        public async Task<IBaseResponse<Invoice>> GetInvoiceId(int id)
        {
            var baseResponse = new BaseResponse<Invoice>();

            try
            {
                var invoice = await _invoiceRepository.Get(id);
                if (invoice == null)
                {
                    baseResponse.Description = "Элемент не найден";
                    baseResponse.StatusCode = StatusCode.OK;

                    return baseResponse;
                }
                baseResponse.Data = invoice;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Invoice>()
                {
                    Description = $"[GetInvoiceId] : {ex.Message}"
                };
            }
        }
        public async Task<IBaseResponse<bool>> DeleteInvoice(int id)
        {
            var baseResponse = new BaseResponse<bool>()
            {
                Data = true
            };

            try
            {
                var invoice = await _invoiceRepository.Get(id);
                if (invoice == null)
                {
                    baseResponse.Description = "Элемент не найден";
                    baseResponse.StatusCode = StatusCode.OK;
                    baseResponse.Data = false;

                    return baseResponse;
                }

                await _invoiceRepository.Delete(invoice);

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteInvoice] : {ex.Message}",
                    StatusCode = StatusCode.OK
                };
            }
        }
        public async Task<IBaseResponse<bool>> Insert(Invoice NewInvoice)
        {
            var baseResponse = new BaseResponse<bool>()
            {
                Data = true
            };

            try
            {
                await _invoiceRepository.Insert(NewInvoice);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateInvoice] : {ex.Message}",
                    StatusCode = StatusCode.OK
                };
            }
        }

        public async Task<IBaseResponse<Invoice>> UpdateInvoice(int id, Invoice NewInvoice)
        {
            var baseResponse = new BaseResponse<Invoice>();
            try
            {
                var invoice = await _invoiceRepository.Get(id);
                if (invoice == null)
                {
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    baseResponse.Description = "Invoice not found";
                    return baseResponse;
                }
                invoice.Informations = new List<Domain.Entity.Information>(NewInvoice.Informations);
                invoice.InvoiceItems = new List<InvoiceItemInfo>(NewInvoice.InvoiceItems);

                await _invoiceRepository.Update(invoice);

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Invoice>()
                {
                    Description = $"[UpdateInvoice] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

		public async Task<IBaseResponse<List<Invoice>>> GetInvoicesByCompanyId(int id)
		{
			var baseResponse = new BaseResponse<List<Invoice>>();
			try
			{
				var invoices = await _invoiceRepository.GetByCompanyId(id);
				if (invoices.Count == 0)
				{
					baseResponse.Description = "Найдено 0 элементов";
					baseResponse.StatusCode = StatusCode.OK;
					return baseResponse;
				}
				baseResponse.Data = invoices;
				baseResponse.StatusCode = StatusCode.OK;

				return baseResponse;
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<Invoice>>()
				{
					Description = $"[GetInvoicesByCompanyId] : {ex.Message}"
				};
			}
		}
	}
}
