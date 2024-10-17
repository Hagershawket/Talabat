using AutoMapper;
using LinkDev.Talabat.Core.Abstraction.Common;
using LinkDev.Talabat.Core.Abstraction.Products;
using LinkDev.Talabat.Core.Abstraction.Products.Models;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Core.Domain.Specifications;
using LinkDev.Talabat.Core.Domain.Specifications.Product_specs;

namespace LinkDev.Talabat.Core.Application.Services.Products
{
    internal class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecParams specParams)
        {
            var specs = new ProductWithBrandAndCategorySpecifications(specParams.Sort, specParams.BrandId, specParams.CategoryId, specParams.PageSize, specParams.PageIndex, specParams.Search );
            var products = await _unitOfWork.getRepository<Product, int>().GetAllWithSpecAsync(specs);
            var mappedProducts = _mapper.Map<IEnumerable<ProductToReturnDto>>(products);

            var countSpec = new ProductWithFilterationForCountSpecifications(specParams.BrandId, specParams.CategoryId, specParams.Search);
            var count = await _unitOfWork.getRepository<Product, int>().GetCountAsync(countSpec);

            return new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, count) { Data = mappedProducts };
        }
        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var specs = new ProductWithBrandAndCategorySpecifications(id);
            var product = await _unitOfWork.getRepository<Product, int>().GetWithSpecAsync(specs);
            if (product is null)
                throw new NotFoundException(nameof(product), id);
            var mappedProduct = _mapper.Map<ProductToReturnDto>(product);
            return mappedProduct;
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
            => _mapper.Map<IEnumerable<BrandDto>>(await _unitOfWork.getRepository<ProductBrand, int>().GetAllAsync());

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
            => _mapper.Map<IEnumerable<CategoryDto>>(await _unitOfWork.getRepository<ProductCategory, int>().GetAllAsync());

    }
}
