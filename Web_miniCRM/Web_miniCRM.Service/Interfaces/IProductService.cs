using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Domain.Response;

namespace Web_miniCRM.Service.Interfaces
{
    public interface IProductService
    {
        Task<IBaseResponse<IEnumerable<Product>>> GetProducts();
        Task<IBaseResponse<Product>> GetProductId(int id);
        Task<IBaseResponse<Product>> GetProductByName(string name);
        Task<IBaseResponse<bool>> DeleteProduct(int id);
        Task<IBaseResponse<bool>> InsertProduct(Product NewCompany);
        Task<IBaseResponse<Product>> UpdateProduct(Product NewCompany);
    }
}
