using Web_miniCRM.Domain.Entity;

namespace Web_miniCRM.DAL.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> GetByName(string name);
    }
}
