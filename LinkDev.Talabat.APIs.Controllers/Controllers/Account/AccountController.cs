using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Core.Abstraction;
using LinkDev.Talabat.Core.Abstraction.Models.Common;
using LinkDev.Talabat.Core.Abstraction.Services.Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Account
{
    public class AccountController(IServiceManager serviceManager) : ApiControllerBase
    {
        [HttpPost("login")] // POST: /api/account/login
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var response = await serviceManager.AuthService.LoginAsync(model);
            return Ok(response);
        }

        [HttpPost("register")] // POST: /api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var response = await serviceManager.AuthService.RegisterAsync(model);
            return Ok(response);
        }

        [Authorize]
        [HttpGet] // GET: /api/account
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var response = await serviceManager.AuthService.GetCurrentUserAsync(User);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("address")] // GET: /api/account/address
        public async Task<ActionResult<AddressDto>> GetAddressUser()
        {
            var response = await serviceManager.AuthService.GetUserAddressAsync(User);
            return Ok(response);
        }
    }
}
