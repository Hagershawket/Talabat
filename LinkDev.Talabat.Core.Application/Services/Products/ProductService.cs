using AutoMapper;
using LinkDev.Talabat.Core.Abstraction.Products;
using LinkDev.Talabat.Core.Abstraction.Products.Models;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Products;

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
        public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync()
            => _mapper.Map<IEnumerable<ProductToReturnDto>>(await _unitOfWork.getRepository<Product, int>().GetAllAsync());
        public async Task<ProductToReturnDto> GetProductAsync(int id)
             => _mapper.Map<ProductToReturnDto>(await _unitOfWork.getRepository<Product, int>().GetAsync(id));

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
            => _mapper.Map<IEnumerable<BrandDto>>(await _unitOfWork.getRepository<ProductBrand, int>().GetAllAsync());

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
            => _mapper.Map<IEnumerable<CategoryDto>>(await _unitOfWork.getRepository<ProductCategory, int>().GetAllAsync());

    }
}
