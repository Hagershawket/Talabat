using AutoMapper;
using LinkDev.Talabat.Core.Abstraction;
using LinkDev.Talabat.Core.Abstraction.Services.Products.Models;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Dashboard.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.Dashboard.Controllers
{
    public class ProductController(IServiceManager _serviceManager, IMapper _mapper) : Controller
    {
        #region Index

        [HttpGet] // GET: /Product/Index
        public async Task<IActionResult> Index()
        {
            var products = await _serviceManager.ProductService.GetProductsWithoutSpecAsync();
            var mappedProducts = _mapper.Map<IReadOnlyList<ProductViewModel>>(products);
            return View(mappedProducts);
        }

        #endregion

        #region Create

        [HttpGet] // GET: /Product/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // POST
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var mappedProduct = _mapper.Map<CreatedProductDto>(model);

            var created = await _serviceManager.ProductService.CreateProductAsync(mappedProduct) > 0;

            return RedirectToAction(nameof(Index));

        }

        #endregion



    }
}
