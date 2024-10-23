using AutoMapper;
using LinkDev.Talabat.Core.Abstraction;
using LinkDev.Talabat.Core.Abstraction.Services.Products.Models;
using LinkDev.Talabat.Dashboard.ViewModels.Brand;
using LinkDev.Talabat.Dashboard.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.Dashboard.Controllers
{
    [Authorize]
    public class BrandController(IServiceManager _serviceManager, IMapper _mapper) : Controller
    {
        #region Index

        [HttpGet] // GET: /Brand/Index
        public async Task<IActionResult> Index()
        {
            var brands = await _serviceManager.ProductService.GetBrandsAsync();
            var mappedBrands = _mapper.Map<IReadOnlyList<BrandViewModel>>(brands);
            return View(mappedBrands);
        }

        #endregion

        #region Create

        [HttpGet] // GET: /Brand/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // POST
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                var mappedBrand = _mapper.Map<CreatedBrandDto>(model);

                var created = await _serviceManager.ProductService.CreateBrandAsync(mappedBrand) > 0;

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Name", "This Name is Already Exists.");
            }

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

            var brand = await _serviceManager.ProductService.GetBrandAsync(id.Value);

            if (brand is null)
                return NotFound($"The Brand with Id {id} is not found");

            var deleted = await _serviceManager.ProductService.DeleteBrandAsync(id.Value);

            if (deleted)
                return RedirectToAction(nameof(Index));

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
