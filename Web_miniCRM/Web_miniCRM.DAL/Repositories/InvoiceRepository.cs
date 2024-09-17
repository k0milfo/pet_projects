using Microsoft.EntityFrameworkCore;
using Web_miniCRM.DAL.Interfaces;
using Web_miniCRM.Domain.Entity;

namespace Web_miniCRM.DAL.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _db;

        public InvoiceRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<bool> Insert(Invoice entity)
        {
            _db.Add(entity);
           await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Invoice entity)
        {
            _db.Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Invoice> Get(int id)
        {
            return await _db.Invoices
            .Include(c => c.InvoiceItems)
            .ThenInclude(i => i.Product)
			.Include(c => c.Informations)
			.FirstOrDefaultAsync(c => c.InvoiceId == id);
        }

        public async Task<List<Invoice>> GetAll()
        {
            return await _db.Invoices.Include(c => c.Company).ToListAsync();
        }

        public async Task<Invoice> Update(Invoice entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

		public async Task<List<Invoice>> GetByCompanyId(int id)
		{
            return await _db.Invoices
                .Where(i => i.CompanyId == id)
                .ToListAsync();
		}
	}
}
