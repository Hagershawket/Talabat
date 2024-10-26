using AutoMapper;
using LinkDev.Talabat.Core.Abstraction;
using LinkDev.Talabat.Core.Abstraction.Common;
using LinkDev.Talabat.Core.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Abstraction.Services.Products.Models;
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
        private readonly IAttachmentService _attachmentService;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IAttachmentService attachmentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _attachmentService = attachmentService;
        }

        #region Product

        #region Get
        public async Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecParams specParams)
        {
            var specs = new ProductWithBrandAndCategorySpecifications(specParams.Sort, specParams.BrandId, specParams.CategoryId, specParams.PageSize, specParams.PageIndex, specParams.Search);
            var products = await _unitOfWork.getRepository<Product, int>().GetAllWithSpecAsync(specs);
            var mappedProducts = _mapper.Map<IEnumerable<ProductToReturnDto>>(products);

            var countSpec = new ProductWithFilterationForCountSpecifications(specParams.BrandId, specParams.CategoryId, specParams.Search);
            var count = await _unitOfWork.getRepository<Product, int>().GetCountAsync(countSpec);

            return new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, count) { Data = mappedProducts };
        }

        public async Task<IEnumerable<ProductToReturnDto>> GetProductsWithoutSpecAsync()
        {
            var products = await _unitOfWork.getRepository<Product, int>().GetAllAsync();
            var mappedProducts = _mapper.Map<IEnumerable<ProductToReturnDto>>(products);
            return mappedProducts;
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
        #endregion

        #region Create

        public async Task<int> CreateProductAsync(CreatedProductDto model)
        {
            var product = _mapper.Map<Product>(model);

            if (model.Image is not null)
                product.PictureUrl = await _attachmentService.UploadFileAsync(model.Image, "products");

            await _unitOfWork.getRepository<Product, int>().AddAsync(product);

            return await _unitOfWork.CompleteAsync();
        }

        #endregion

        #region Update

        public async Task<int> UpdateProductAsync(UpdatedProductDto model)
        {
            var product = _mapper.Map<Product>(model);
            product.CreatedBy = "Temp";

            if (model.Image is not null)
            {
                _attachmentService.DeleteFile(model.PictureUrl!);
                product.PictureUrl = await _attachmentService.UploadFileAsync(model.Image, "products");
            }

            _unitOfWork.getRepository<Product, int>().Update(product);

            return await _unitOfWork.CompleteAsync();
        }

        #endregion

        #region Delete

        public async Task<bool> DeleteProductAsync(int id)
        {
            var productRepo = _unitOfWork.getRepository<Product,int>();
            var product = await productRepo.GetAsync(id);

            if (product is not null && product.PictureUrl is not null)
            {
                _attachmentService.DeleteFile(product.PictureUrl);
                productRepo.Delete(product);
            }
                
            return await _unitOfWork.CompleteAsync() > 0;
        }

        #endregion

        #endregion

        #region Brand

        #region Get

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
            => _mapper.Map<IEnumerable<BrandDto>>(await _unitOfWork.getRepository<ProductBrand, int>().GetAllAsync());

        public async Task<BrandDto> GetBrandAsync(int id)
           => _mapper.Map <BrandDto> (await _unitOfWork.getRepository<ProductBrand, int>().GetAsync(id));

        #endregion

        #region Create

        public async Task<int> CreateBrandAsync(CreatedBrandDto model)
        {
            var brand = _mapper.Map<ProductBrand>(model);

            await _unitOfWork.getRepository<ProductBrand, int>().AddAsync(brand);

            return await _unitOfWork.CompleteAsync();
        }

        #endregion

        #region Delete
        public async Task<bool> DeleteBrandAsync(int id)
        {
            var brandRepo = _unitOfWork.getRepository<ProductBrand, int>();
            var brand = await brandRepo.GetAsync(id);

            if (brand is not null)
                brandRepo.Delete(brand);

            return await _unitOfWork.CompleteAsync() > 0;
        }

        #endregion

        #endregion

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
            => _mapper.Map<IEnumerable<CategoryDto>>(await _unitOfWork.getRepository<ProductCategory, int>().GetAllAsync());

    }
}
