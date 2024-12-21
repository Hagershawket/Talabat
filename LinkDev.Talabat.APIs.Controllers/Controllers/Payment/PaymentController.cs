using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Payment
{
    [Authorize]
    public class PaymentController(IPaymentService paymentService) : ApiControllerBase
    {
        [HttpPost("{basketId}")] // POST: /api/payment/{basketId}
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var result = await paymentService.CreateOrUpdatePaymentIntent(basketId);
            return Ok(result);
        }
    }
}
