using Web_miniCRM.Domain.Entity;

namespace Web_miniCRM.DAL.Interfaces
{
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
		Task<List<Invoice>> GetByCompanyId(int id);
	}
}
