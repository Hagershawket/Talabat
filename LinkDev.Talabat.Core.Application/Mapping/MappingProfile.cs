using AutoMapper;
using LinkDev.Talabat.Core.Abstraction.Models.Common;
using LinkDev.Talabat.Core.Abstraction.Models.Orders;
using LinkDev.Talabat.Core.Abstraction.Services.Basket.Models;
using LinkDev.Talabat.Core.Abstraction.Services.Products.Models;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using LinkDev.Talabat.Core.Domain.Entities.Order;
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

            #region Basket

            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

            #endregion

            #region Order

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(D => D.DeliveryMethod, config => config.MapFrom(S => S.DeliveryMethod!.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.ProductId, config => config.MapFrom(S => S.Product!.ProductId))
                .ForMember(D => D.ProductName, config => config.MapFrom(S => S.Product!.ProductName))
                .ForMember(D => D.PictureUrl, opt => opt.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<Address, AddressDto>();

            CreateMap<DeliveryMethod, DeliveryMethodDto>();

            #endregion
        }

    }
}
