using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Core.Abstraction;
using LinkDev.Talabat.Core.Abstraction.Services.Auth.Models;
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
    }
}
