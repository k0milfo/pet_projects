using Microsoft.EntityFrameworkCore;
using Web_miniCRM.DAL.Interfaces;
using Web_miniCRM.Domain.Entity;

namespace Web_miniCRM.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Insert(Product entity)
        {
            _db.Add(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Product entity)
        {
            _db.Remove(entity);
            _db.SaveChangesAsync();
            return true;
        }

        public async Task<Product> Get(int id)
        {
            return await _db.Products
            .Include(i => i.Informations)
            .Include(ii => ii.InvoiceItems)
            .FirstOrDefaultAsync(iii => iii.ProductId == id);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> GetByName(string name)
        {
            return await _db.Products.FirstOrDefaultAsync(i => i.Name.Equals(name));
        }

        public async Task<Product> Update(Product entity)
        {

            _db.Update(entity);
            _db.SaveChangesAsync();
            return entity;
        }
    }
}
