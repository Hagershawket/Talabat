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

        #region Update

        [HttpGet]   // GET: /Product/Edit/id?
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var product = await _serviceManager.ProductService.GetProductAsync(id.Value);

            if (product is null)
                return NotFound($"The Product with Id {id} is not found");

            var productVM = _mapper.Map<ProductViewModel>(product);

            return View(productVM);
        }

        [HttpPost]   // POST
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, ProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var product = _mapper.Map<UpdatedProductDto>(model);

            product.Id = id;

            var updated = await _serviceManager.ProductService.UpdateProductAsync(product) > 0;

            if (updated)
               return RedirectToAction(nameof(Index));

            return View(model);
        }

        #endregion

        #region Delete

        [HttpPost]  // POST
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var product = await _serviceManager.ProductService.GetProductAsync(id.Value);

            if(product is null)
                return NotFound($"The Product with Id {id} is not found");

            var deleted = await _serviceManager.ProductService.DeleteProductAsync(id.Value);

            if (deleted)
                return RedirectToAction(nameof(Index));

            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}
