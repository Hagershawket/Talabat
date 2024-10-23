using AutoMapper;
using LinkDev.Talabat.Core.Abstraction.Services.Products.Models;
using LinkDev.Talabat.Dashboard.ViewModels.Product;

namespace LinkDev.Talabat.Dashboard.Mapping
{
    public class MvcMappingProfile : Profile
    {
        public MvcMappingProfile()
        {
            #region Product

            CreateMap<ProductToReturnDto, ProductViewModel>();
            CreateMap<ProductViewModel, CreatedProductDto>();
            CreateMap<ProductViewModel, UpdatedProductDto>();

            #endregion

        }
    }
}
