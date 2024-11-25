using LinkDev.Talabat.Core.Abstraction.Models.Common;
using LinkDev.Talabat.Core.Abstraction.Services.Auth.Models;
using System.Security.Claims;

namespace LinkDev.Talabat.Core.Abstraction.Services.Auth
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(LoginDto model);
        Task<UserDto> RegisterAsync(RegisterDto model);
        Task<UserDto> GetCurrentUserAsync(ClaimsPrincipal claimsPrincipal);
        Task<AddressDto> GetUserAddressAsync(ClaimsPrincipal claimsPrincipal);
    }
}
