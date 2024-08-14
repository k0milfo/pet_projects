using Microsoft.AspNetCore.Mvc;
using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Service.Interfaces;

namespace Web_miniCRM.Controllers
{
    public class ProductController : Controller
    {
        public readonly IProductService _productServices;

        public ProductController(IProductService productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var response = await _productServices.GetProducts();

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetProductId(int id)
        {
            var response = await _productServices.GetProductId(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _productServices.DeleteProduct(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetCompanies");
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> UpsertProduct(Product model)
        {
            if (ModelState.IsValid)
            {
                if (model.ProductId == 0)
                {
                await _productServices.InsertProduct(model);
                }
                else
                {
                    await _productServices.UpdateProduct(model);
                }
            }
            return RedirectToAction("GetProducts");
        }
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var NewProduct = new Product();
            NewProduct.Informations = new List<Information>();
            NewProduct.Informations.Add(new Information { Details = "" });

            return View(NewProduct);
        }
    }
}
