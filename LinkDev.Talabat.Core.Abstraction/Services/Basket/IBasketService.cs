using LinkDev.Talabat.Core.Abstraction.Services.Basket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Abstraction.Services.Basket
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetCustomerBasketAsync(string basketId);
        Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto basketDto);
        Task DeleteCustomerBasketAsync(string basketId);
    }
}
