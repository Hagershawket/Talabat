using LinkDev.Talabat.Core.Abstraction.Common;
using LinkDev.Talabat.Core.Abstraction.Services.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Abstraction.Services.Products
{
    public interface IProductService
    {
        Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecParams specParams);
        Task<ProductToReturnDto> GetProductAsync(int id);
        Task<IEnumerable<BrandDto>> GetBrandsAsync();
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();


    }
}
