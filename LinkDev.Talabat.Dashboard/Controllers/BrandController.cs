using AutoMapper;
using LinkDev.Talabat.Core.Abstraction;
using LinkDev.Talabat.Dashboard.ViewModels.Brand;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.Dashboard.Controllers
{
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


    }
}
