using Microsoft.EntityFrameworkCore;
using Web_miniCRM.DAL.Interfaces;
using Web_miniCRM.Domain.Entity;

namespace Web_miniCRM.DAL.Repositories
{
    public class HeadDepartmentRepository : IHeadDepartmentRepository
    {
        private readonly ApplicationDbContext _db;
        public HeadDepartmentRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<bool> Delete(HeadDepartment entity)
        {
            _db.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<HeadDepartment> Get(int id)
        {
            return await _db.HeadDepartments.FirstOrDefaultAsync(i => i.HeadDepartmentId == id);
        }

        public async Task<List<HeadDepartment>> GetAll()
        {
            return await _db.HeadDepartments.ToListAsync();
        }

        public async Task<bool> Insert(HeadDepartment entity)
        {
            _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<HeadDepartment> Update(HeadDepartment entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
