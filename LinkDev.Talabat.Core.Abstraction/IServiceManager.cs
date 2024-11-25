using LinkDev.Talabat.Core.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Abstraction.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Abstraction
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
        public IAuthService AuthService { get; }
        public IOrderService OrderService { get; }
    }
}
