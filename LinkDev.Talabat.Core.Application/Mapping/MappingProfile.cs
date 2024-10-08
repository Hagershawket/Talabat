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

            CreateMap<Product, ProductToReturnDto>();

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
