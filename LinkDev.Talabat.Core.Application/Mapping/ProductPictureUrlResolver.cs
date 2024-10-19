using AutoMapper;
using LinkDev.Talabat.Core.Abstraction.Services.Products.Models;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Mapping
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string?>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string? Resolve(Product source,ProductToReturnDto destination, string? destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }

            return $"{_configuration["Urls:ApiBaseUrl"]}/{source.PictureUrl}";
        }
    }
}
