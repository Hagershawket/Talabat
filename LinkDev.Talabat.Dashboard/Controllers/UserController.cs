using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Dashboard.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Dashboard.Controllers
{
    public class UserController(UserManager<ApplicationUser> _userManager) : Controller
    {
        #region Index

        [HttpGet] // GET: /User/Index
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName!,
                    Email = user.Email!,
                    DisplayName = user.DisplayName,
                    PhoneNumber = user.PhoneNumber!,
                    Roles = roles
                });
            }

            return View(userViewModels);
        }

        #endregion
    }
}
