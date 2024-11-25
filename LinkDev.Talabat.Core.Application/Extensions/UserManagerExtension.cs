using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LinkDev.Talabat.Core.Application.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<ApplicationUser?> FindUserWithAddress(this UserManager<ApplicationUser> userManager, ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

            var user = userManager.Users.Where(user => user.Email == email).Include(user => user.Address).FirstOrDefault();

            return user;
        }
    }
}
