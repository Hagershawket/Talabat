using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Core.Abstraction;
using LinkDev.Talabat.Core.Abstraction.Products.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Products
{
    public class ProductController(IServiceManager serviceManager) : ApiControllerBase
    {
        [HttpGet] // GET: /api/Product
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts(string? sort, int? brandId, int? categoryId)
        {
            var products = await serviceManager.ProductService.GetProductsAsync(sort, brandId, categoryId);
            return Ok(products);
        }

        [HttpGet("{id}")] // GET: /api/Product/id
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts(int id)
        {
            var product = await serviceManager.ProductService.GetProductAsync(id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("brands")] // GET: /api/Product/brands
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var brands = await serviceManager.ProductService.GetBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("categories")] // GET: /api/Product/categories
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var Categories = await serviceManager.ProductService.GetCategoriesAsync();
            return Ok(Categories);
        }

    }
}
