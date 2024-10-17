using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Core.Abstraction;
using LinkDev.Talabat.Core.Abstraction.Services.Basket.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Basket
{
    public class BasketController(IServiceManager serviceManager) : ApiControllerBase
    {
        [HttpGet]  // GET: /api/basket?id=
        public async Task<ActionResult<CustomerBasketDto>> GetBasket(string id)
        {
            var basket = await serviceManager.BasketService.GetCustomerBasketAsync(id);
            return Ok(basket);
        }

        [HttpPost]  // POST: /api/basket
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basketDto)
        {
            var basket = await serviceManager.BasketService.UpdateCustomerBasketAsync(basketDto);
            return Ok(basket);
        }

        [HttpDelete]  // DELETE: /api/basket
        public async Task DeleteBasket(string id) => await serviceManager.BasketService.DeleteCustomerBasketAsync(id);
    }
}
