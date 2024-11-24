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
    }
}
