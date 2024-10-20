using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Dashboard.ViewModels.Role;
using LinkDev.Talabat.Dashboard.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LinkDev.Talabat.Dashboard.Controllers
{
    public class UserController(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager) : Controller
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

        #region Edit

        [HttpGet]   // GET: /User/Edit/id?
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();

            var roles = await _roleManager.Roles.ToListAsync();

            var viewModel = new UpdatedUserViewModel
            {
                UserName = user.UserName!,
                Roles = roles.Select(R => new RoleViewModel
                {
                    Id = R.Id,
                    Name = R.Name!,
                    IsSelected = _userManager.IsInRoleAsync(user, R.Name!).Result
                }).ToList()
            };

            return View(viewModel);
        }


        [HttpPost] // POST
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UpdatedUserViewModel model)
        {
            if (id is null)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if (userRoles.Any(R => R == role.Name) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.Name);

                if (!userRoles.Any(R => R == role.Name) && role.IsSelected)
                    await _userManager.AddToRoleAsync(user,role.Name);
            }

            return RedirectToAction(nameof(Index));

        }

        #endregion
    }
}
