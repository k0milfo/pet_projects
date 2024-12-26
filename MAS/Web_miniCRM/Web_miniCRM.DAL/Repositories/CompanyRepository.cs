using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq;
using Web_miniCRM.DAL.Interfaces;
using Web_miniCRM.Domain.Entity;

namespace Web_miniCRM.DAL.Repositories
{
	public class CompanyRepository : ICompanyRepository
	{
		private readonly ApplicationDbContext _db;
		public CompanyRepository(ApplicationDbContext dbContext)
		{
			_db = dbContext;

		}

		public async Task<bool> Insert(Company entity)
		{
			_db.Add(entity);
			await _db.SaveChangesAsync();
			return true;
		}

		public async Task<bool> Delete(Company entity)
		{
			_db.Remove(entity);
			await _db.SaveChangesAsync();

			return true;
		}

		public async Task<List<Company>> GetAll()
		{
			return await _db.Companies
				.Include(i => i.Manager)
				.ToListAsync();
		}

		public async Task<Company> Get(int id)
		{
			return await _db.Companies
				.Include(i => i.Manager)
            .Include(i => i.Informations)
            .Include(i => i.Calls)
			.Include(i => i.Meetings)
			.Include(i => i.Invoices)
				.ThenInclude(i => i.InvoiceItems)
					.ThenInclude(ii => ii.Product)
			.FirstOrDefaultAsync(c => c.CompanyId == id);


			//.Include(c => c.Informations)
			//.Include(c => c.Invoices)
			//    .ThenInclude(i => i.InvoiceItems)
			//.Include(c => c.Invoices)
			//    .ThenInclude(i => i.Informations)
			//.FirstOrDefaultAsync(c => c.CompanyId == id);
		}
		public async Task<Company> GetByName(string name)
		{
			return await _db.Companies.FirstOrDefaultAsync(item => item.CompanyName.Equals(name));
		}
		public async Task<Company> Update(Company entity)
		{
			_db.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
