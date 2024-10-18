using LinkDev.Talabat.Core.Abstraction.Services.Auth.Models;

namespace LinkDev.Talabat.Core.Abstraction.Services.Auth
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(LoginDto model);
        Task<UserDto> RegisterAsync(RegisterDto model);
    }
}
