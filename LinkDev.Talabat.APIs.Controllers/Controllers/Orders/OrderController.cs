using LinkDev.Talabat.Core.Abstraction.Models.Orders;
using LinkDev.Talabat.Core.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using LinkDev.Talabat.APIs.Controllers.Controllers.Base;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Orders
{
    [Authorize]
    public class OrderController(IServiceManager _serviceManager) : ApiControllerBase
    {
        [HttpPost]  // POST:  /api/order
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderToCreateDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            
            var result = await _serviceManager.OrderService.CreateOrderAsync(buyerEmail!, orderDto);

            return Ok(result);
        }

        [HttpGet]   // GET: /api/Order
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var result = await _serviceManager.OrderService.GetOrdersForUserAsync(buyerEmail!);

            return Ok(result);
        }

        [HttpGet("{id}")]   // GET: /api/Order/{id}
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var result = await _serviceManager.OrderService.GetOrderByIdAsync(buyerEmail!, id);

            return Ok(result);
        }

        [HttpGet("deliveryMethods")]   // GET: /api/Order/deliveryMethods
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var result = await _serviceManager.OrderService.GetDeliveryMethodsAsync();

            return Ok(result);
        }
    }
}
