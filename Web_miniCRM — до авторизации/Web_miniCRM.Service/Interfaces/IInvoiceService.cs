using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Domain.Response;

namespace Web_miniCRM.Service.Interfaces
{
    public interface IInvoiceService
    {

        Task<IBaseResponse<List<Invoice>>> GetInvoices();
		Task<IBaseResponse<List<Invoice>>> GetInvoicesByCompanyId(int id);
		Task<IBaseResponse<Invoice>> GetInvoiceId(int id);
        Task<IBaseResponse<bool>> DeleteInvoice(int id);
        Task<IBaseResponse<bool>> Insert(Invoice NewInvoice);
        Task<IBaseResponse<Invoice>> UpdateInvoice(int id, Invoice NewInvoice);
    }
}
