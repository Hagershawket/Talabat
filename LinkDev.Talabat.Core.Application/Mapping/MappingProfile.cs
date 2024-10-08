using AutoMapper;
using LinkDev.Talabat.Core.Abstraction.Products.Models;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Mapping
{
    internal class MappingProfile : Profile
    {        
        public MappingProfile() 
        {
            #region Product

            CreateMap<Product, ProductToReturnDto>()
                .ForMember(D => D.Brand, config => config.MapFrom(S => S.Brand!.Name))
                .ForMember(D => D.Category, config => config.MapFrom(S => S.Category!.Name))
                .ForMember(d => d.PictureUrl, opt => opt.MapFrom<ProductPictureUrlResolver>());

            #endregion

            #region Brand

            CreateMap<ProductBrand, BrandDto>();

            #endregion

            #region Category

            CreateMap<ProductCategory, CategoryDto>();

            #endregion
        }

    }
}
