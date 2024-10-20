using LinkDev.Talabat.Core.Abstraction.Services.Auth.Models;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.Dashboard.Controllers
{
    public class AdminController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager) : Controller
    {
        #region Login

        [HttpGet] // GET: /Admin/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost] // Post
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is { })
            {
                var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                if (flag)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);

                    if (result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your Account is not confimed yet!!");
                    if (result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your Account is locked!!");
                    if (result.Succeeded && await _userManager.IsInRoleAsync(user, "Admin"))
                        return RedirectToAction(nameof(HomeController.Index), "Home");

                }
                else
                    ModelState.AddModelError(string.Empty, "Invalid Login");
            }
            else
                ModelState.AddModelError(string.Empty, "Invalid Login");

            return View(model);
        }
        #endregion
    }
}
