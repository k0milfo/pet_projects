using Web_miniCRM.DAL.Interfaces;
using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Domain.Enum;
using Web_miniCRM.Domain.Response;
using Web_miniCRM.Service.Interfaces;

namespace Web_miniCRM.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IBaseResponse<bool>> DeleteProduct(int id)
        {
            var baseResponse = new BaseResponse<bool>()
            {
                Data = true
            };

            try
            {
                var company = await _productRepository.Get(id);
                if (company == null)
                {
                    baseResponse.Description = "Элемент не найден";
                    baseResponse.StatusCode = StatusCode.OK;
                    baseResponse.Data = false;

                    return baseResponse;
                }

                await _productRepository.Delete(company);

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteProduct] : {ex.Message}",
                    StatusCode = StatusCode.OK
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Product>>> GetProducts()
        {
            var baseResponse = new BaseResponse<IEnumerable<Product>>();

            try
            {
                var products = await _productRepository.GetAll();
                if (products.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                baseResponse.Data = products;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Product>>()
                {
                    Description = $"[DeleteProduct] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<Product>> GetProductByName(string name)
        {
            var baseResponse = new BaseResponse<Product>();
            try
            {
                var product = await _productRepository.GetByName(name);
                if (product == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;

                    return baseResponse;
                }
                baseResponse.Data = product;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[GetByName]{ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<Product>> GetProductId(int id)
        {
            var baseResponse = new BaseResponse<Product>();
            try
            {
                var product = await _productRepository.Get(id);
                if (product == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                baseResponse.Data = product;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[GetProductId] - {ex.Message}",
                };
            }
        }

        public async Task<IBaseResponse<bool>> InsertProduct(Product NewProduct)
        {
            var baseResponse = new BaseResponse<bool>()
            {
                Data = true
            };
            try
            {
                await _productRepository.Insert(NewProduct);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = $"[InsertProduct] : {ex.Message}",
                    StatusCode = StatusCode.OK
                };
            }
        }

        public async Task<IBaseResponse<Product>> UpdateProduct(Product NewProduct)
        {
            var baseResponse = new BaseResponse<Product>();

            try
            {
                var product = await _productRepository.Get(NewProduct.ProductId);

                if(product == null)
                {
                    baseResponse.StatusCode= StatusCode.InternalServerError;
                    baseResponse.Description = "Product not found";
                    return baseResponse;
                }

                product.Name = NewProduct.Name;
                product.Price = NewProduct.Price;
                product.CountAllProduct = NewProduct.CountAllProduct;
                product.Informations = new List<Information>(NewProduct.Informations);

                await _productRepository.Update(product);
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Description = "Product обновлен";
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[UpdateProduct] - {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
